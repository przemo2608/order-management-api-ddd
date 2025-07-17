using OrderManagement.Domain.Common;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.ValueObjects;

public sealed class OrderItem(Product product, Quantity quantity) : ValueObject
{
    public ProductId ProductId { get; } = product.Id;

    public ProductName ProductName { get; } = product.Name;

    public Price UnitPrice { get; } = product.Price;

    public Quantity Quantity { get; } = quantity;

    public Price TotalPrice => new(UnitPrice.Value * Quantity.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
    }
}
