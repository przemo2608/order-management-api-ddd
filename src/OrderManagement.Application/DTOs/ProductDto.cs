using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.DTOs;

public record ProductDto(
Guid Id,
string Name,
string Description,
decimal Price)
{
    public static ProductDto FromProduct(Product product)
    {
        return new ProductDto(
            Id: product.Id.Value,
            Name: product.Name.Value,
            Description: product.Description.Value,
            Price: product.Price.Value
        );
    }
}
