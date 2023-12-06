using AutoMapper;
using BusinessObjects.DataModels;
using MenuMinderAPI.MiddleWares;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.Implementations;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Logging.AddConsole();

// Configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// builder.Services.AddScoped<GlobalExceptionMiddleware>();

// configure DI for application repositories
builder.Services.AddScoped<DiningTableRepository, DiningTableRepository>();
builder.Services.AddScoped<FoodRepository, FoodRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();

// configure DI for DBContext
builder.Services.AddScoped<Menu_minder_dbContext, Menu_minder_dbContext>();

// configure DI for application services
builder.Services.AddScoped<DiningTableService, DiningTableService>();
builder.Services.AddScoped<FoodService, FoodService>();
builder.Services.AddScoped<CategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
