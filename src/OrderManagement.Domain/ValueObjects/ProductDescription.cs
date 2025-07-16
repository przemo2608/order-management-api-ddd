using OrderManagement.Domain.Common;

namespace OrderManagement.Domain.ValueObjects;

public sealed class ProductDescription(string value) : ValueObject
{
    public string Value { get; } = value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductDescription name) => name.Value;
    public static implicit operator ProductDescription(string value) => new(value);
}
