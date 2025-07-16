using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }

    public ProductId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Order ID cannot be empty");

        Value = value;
    }

    public static ProductId New() => new(Guid.NewGuid());

    public static implicit operator Guid(ProductId id) => id.Value;
    public static implicit operator ProductId(Guid value) => new(value);
}
