using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities;

public class Product
{
    public ProductId Id { get; private set; }

    public string Name { get; private set; }

    public Money Price { get; private set; }

    public Product(ProductId id, string name, Money price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        Id = id;
        Name = name;
        Price = price;
    }
}
