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
}
