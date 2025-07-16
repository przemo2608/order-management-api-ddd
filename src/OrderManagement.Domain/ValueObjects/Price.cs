using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public sealed class Price : ValueObject
{
    public decimal Value { get; }

    public Price(decimal value)
    {
        if (value < 0)
            throw new DomainException("Price cannot be negative");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator decimal(Price price) => price.Value;
    public static implicit operator Price(decimal value) => new(value);

    public static Price operator +(Price a, Price b) => new(a.Value + b.Value);
    public static Price operator -(Price a, Price b) => new(a.Value - b.Value);
    public static Price operator *(Price price, Quantity quantity) => new(price.Value * quantity.Value);

    public override string ToString() => Value.ToString("C");
}
