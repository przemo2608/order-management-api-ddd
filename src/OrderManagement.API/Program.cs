using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OrderManagement.API.Extensions;
using OrderManagement.Application.Configuration;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services
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

app.MapEndpoints();

app.SeedDatabase();

app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception == null) return;

        var problem = new ProblemDetails
        {
            Title = "An error occurred",
            Status = context.Response.StatusCode,
            Detail = exception.Message
        };

        switch (exception)
        {
            case DomainException domainEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Domain validation error";
                break;

            case KeyNotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                problem.Status = StatusCodes.Status404NotFound;
                problem.Title = "Resource not found";
                break;
        }

        await context.Response.WriteAsJsonAsync(problem);
    }));

app.Run();
