using OrderManagement.API.Endpoints.Orders;
using OrderManagement.API.Endpoints.Products;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            // Orders
            app.MapPost("/orders", CreateOrderEndpoint.Handle);
            app.MapPost("/orders/{id}/items", AddItemToOrderEndpoint.Handle);
            app.MapPut("/orders/{id}/status", UpdateOrderStatusEndpoint.Handle);
            app.MapGet("/orders/{id}", GetOrderEndpoint.Handle);

            // Products
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
    }
}
