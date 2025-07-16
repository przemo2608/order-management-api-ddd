using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    public OrderId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Order ID cannot be empty");

        Value = value;
    }

    public static OrderId New() => new(Guid.NewGuid());

    public static implicit operator Guid(OrderId id) => id.Value;
    public static implicit operator OrderId(Guid value) => new(value);
}
