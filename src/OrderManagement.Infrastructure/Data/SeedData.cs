using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Infrastructure.Data;

public static class SeedData
{
    public static IEnumerable<Product> GetInitialProducts()
    {
        return
    [
        new(
            id: new ProductId(Guid.Parse("11111111-1111-1111-1111-111111111111")),
            name: new ProductName("Ball"),
            price: new Price(100.00m),
            description: new ProductDescription("A ball for playing football.")
        ),
        new(
            id: new ProductId(Guid.Parse("22222222-2222-2222-2222-222222222222")),
            name: new ProductName("Shoes"),
            price: new Price(350.00m),
            description: new ProductDescription("Shoes for running.")
        ),
        new(
            id: new ProductId(Guid.Parse("33333333-3333-3333-3333-333333333333")),
            name: new ProductName("Gloves"),
            price: new Price(80.00m),
            description: new ProductDescription("Gloves for weight lifting.")
        ),
        new(
            id: new ProductId(Guid.Parse("44444444-4444-4444-4444-444444444444")),
            name: new ProductName("Bottle"),
            price: new Price(30.00m),
            description: new ProductDescription("A bottle for drinking water.")
        ),
        new(
            id: new ProductId(Guid.Parse("55555555-5555-5555-5555-555555555555")),
            name: new ProductName("Mat"),
            price: new Price(120.00m),
            description: new ProductDescription("A mat for doing yoga.")
        )
    ];
    }
}
