using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Infrastructure.Data;

public static class SeedData
{
    public static IEnumerable<Product> GetInitialProducts()
    {
        return new List<Product>
    {
        new Product(
            id: new ProductId(Guid.Parse("11111111-1111-1111-1111-111111111111")),
            name: new ProductName("Laptop"),
            price: new Price(3000.00m),
            description: new ProductDescription("Powerful laptop with 16GB RAM and 1TB SSD")
        ),
        new Product(
            id: new ProductId(Guid.Parse("22222222-2222-2222-2222-222222222222")),
            name: new ProductName("Smartphone"),
            price: new Price(2500.00m),
            description: new ProductDescription("Latest smartphone with 5G and 128GB storage")
        ),
        new Product(
            id: new ProductId(Guid.Parse("33333333-3333-3333-3333-333333333333")),
            name: new ProductName("Tablet"),
            price: new Price(1500.00m),
            description: new ProductDescription("10-inch tablet with high-resolution display")
        ),
        new Product(
            id: new ProductId(Guid.Parse("44444444-4444-4444-4444-444444444444")),
            name: new ProductName("Headphones"),
            price: new Price(500.00m),
            description: new ProductDescription("Noise-cancelling wireless headphones")
        ),
        new Product(
            id: new ProductId(Guid.Parse("55555555-5555-5555-5555-555555555555")),
            name: new ProductName("Smart Watch"),
            price: new Price(1200.00m),
            description: new ProductDescription("Water-resistant smartwatch with health monitoring")
        )
    };
    }
}
