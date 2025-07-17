using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Endpoints.Orders;
using OrderManagement.API.Endpoints.Products;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.API.Extensions;

public static class WebApplicationExtensionsjak
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapPost("/orders", CreateOrderEndpoint.Handle);
        app.MapPost("/orders/{orderId}/items", AddItemToOrderEndpoint.Handle);
        app.MapPut("/orders/{orderId}/status", UpdateOrderStatusEndpoint.Handle);
        app.MapGet("/orders/{orderId}", GetOrderEndpoint.Handle);

        app.MapGet("/products", GetAllProductsEndpoint.Handle);

        return app;
    }

    public static WebApplication SeedDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            var products = SeedData.GetInitialProducts();
            productRepository.InitializeAsync(products).Wait();
        }
        return app;
    }

    public static WebApplication AddGlobalExceptionHandling(this WebApplication app)
    {
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

        return app;
    }
}
