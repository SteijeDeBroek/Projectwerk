using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Design;
using System.Text;
using Microsoft.OpenApi.Models;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Cookiemonster.Infrastructure.Repositories;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Cookiemonster.API;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using AspNetCoreRateLimit;

using Serilog;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);



builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console());




{
    Console.WriteLine("Cors active");
    // Adding CORS Policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:8000") // Add the client's origin
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));

                          });
    });
}
// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IRepository<Recipe>, RecipeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IRepository<Todo>, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository<Vote>, VoteRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheTestService", Version = "v1" });
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });

    //////Add Operation Specific Authorization///////
    c.OperationFilter<AuthOperationFilter>();
    ////////////////////////////////////////////////
});
builder.Services.AddRazorPages();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    setup.DisableDatabaseMigrations();
    setup.MaximumHistoryEntriesPerEndpoint(50);
    setup.AddHealthCheckEndpoint("EFCore connection", "/healthz");
}).AddInMemoryStorage();

// RATE LIMITING SERVICES
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();





// For JWT:
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // default scheme
    .AddJwtBearer(
    authenticationScheme: JwtBearerDefaults.AuthenticationScheme, // Bearer
    configureOptions: options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:PrivateKey"]))
        };
    });
builder.Services.AddAuthorization();







//Create an instance of IConfiguration and load the appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Cookiemonster"));
});


// na builder.Services.AddDbContext():
//builder.Services.AddHealthChecks().AddDbContextCheck<SisDbContext>();      // brad of ilya, kijk hier eens naar
// AddCheck<DbContextHealthCheck<SisDbContext>>("SisDbContextHealthCheck");

builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    setup.DisableDatabaseMigrations();
    //setup.SetEvaluationTimeInSeconds(5); // Configures the UI to poll for health checks updates every 5 seconds
    //setup.SetApiMaxActiveRequests(1); //Only one active request will be executed at a time. All the excedent requests will result in 429 (Too many requests)
    setup.MaximumHistoryEntriesPerEndpoint(50); // Set the maximum history entries by endpoint that will be served by the UI api middleware
    //setup.SetNotifyUnHealthyOneTimeUntilChange(); // You will only receive one failure notification until the status changes

    setup.AddHealthCheckEndpoint("EFCore connection", "/working");

}).AddInMemoryStorage();

//var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions
//{
//    SslProtocols = SslProtocols.Tls12,
//    ClientCertificateMode = ClientCertificateMode.AllowCertificate,
//    ServerCertificate = new X509Certificate2("./certificate.pfx", "password") //password ontbreekt, ilya?
//};

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ConfigureEndpointDefaults(listenOptions =>
//    {
//        listenOptions.UseHttps(httpsConnectionAdapterOptions);
//    });
//});




var app = builder.Build();

// Configureer de Health Check response format
var healthCheckOptions = new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(
            new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new { name = e.Key, status = e.Value.Status.ToString(), exception = e.Value.Exception?.Message, duration = e.Value.Duration })
            });
        await context.Response.WriteAsync(result);
    }
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<IPWhitelistMiddleware>();
    app.Logger.LogDebug("Development mode");
    app.MapSwagger();
    app.UseSwaggerUI();
    app.UseIpRateLimiting();
};

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
//{
//  app.UseExceptionHandler("/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// app.UseHsts();
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthz", healthCheckOptions);
    endpoints.MapHealthChecksUI();
});




app.UseAuthentication(); // for JWT


app.MapRazorPages();
app.MapControllers();
// For React client:

app.Run();
