using Cookiemonster.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Cookiemonster.Interfaces;
using Cookiemonster.Models;


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
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();



//Create an instance of IConfiguration and load the appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
   
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
// For React client:
app.UseCors("AllowOrigin");

app.Run();
