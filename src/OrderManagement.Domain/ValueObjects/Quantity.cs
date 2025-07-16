using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public sealed class Quantity : ValueObject
{
    public int Value { get; }

    public Quantity(int value)
    {
        if (value <= 0)
            throw new DomainException("Quantity must be positive");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
