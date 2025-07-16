using OrderManagement.Domain.Common;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities;

public class Product(ProductId id, ProductName name, Price price, ProductDescription description) : Entity<ProductId>(id)
{
    public ProductName Name { get; } = name;
    public ProductDescription Description { get; } = description;
    public Price Price { get; } = price;
}
