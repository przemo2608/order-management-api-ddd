using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public sealed class ProductName : ValueObject
{
    public string Value { get; }

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Product name cannot be empty");

        if (value.Length > 100)
            throw new DomainException("Product name is too long");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
