using Microsoft.OpenApi.Models;
using OrderManagement.API.Extensions;
using OrderManagement.Application.Configuration;
using OrderManagement.Domain.Configuration;
using OrderManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Management API",
        Version = "v1",
        Description = "API for managing orders and products"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.AddGlobalExceptionHandling();

app.MapEndpoints();

app.SeedDatabase();

app.Run();
