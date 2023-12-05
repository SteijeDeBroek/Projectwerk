using Cookiemonster.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);




// Voor REACT client toegevoegd:
{
    Console.WriteLine("Cors active");
    // Adding CORS Policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowOrigin", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
        });
    });
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IRepository<Recipe>, RecipeRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IRepository<Todo>, TodoRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
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
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));


});


// na builder.Services.AddDbContext():
builder.Services.AddHealthChecks().AddDbContextCheck<SisDbContext>();      // brad of ilya, kijk hier eens naar
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

// Na app.UseHttpsRedirection(), voor app.UseSwaggerResponseCheck() en app.MapControllers():

// to print json:
var options = new HealthCheckOptions
{
    ResponseWriter = async (c, r) =>
    {
        c.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            status = r.Status.ToString(),
            errors = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
        });
        await c.Response.WriteAsync(result);
    }
};






var app = builder.Build();

app.UseHealthChecks("/working", options);

// url: /healthchecks-ui
app.UseRouting().UseEndpoints(config =>
{
    config.MapHealthChecksUI();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Logger.LogDebug("Development mode");
    app.MapSwagger();
    app.UseSwaggerUI();
}

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

app.UseAuthentication(); // for JWT
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
// For React client:
app.UseCors("AllowOrigin");

app.Run();
