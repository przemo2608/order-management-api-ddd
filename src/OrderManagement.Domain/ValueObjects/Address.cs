using OrderManagement.Domain.Common;

namespace OrderManagement.Domain.ValueObjects;

public sealed class Address(string street, string city, string postalCode) : ValueObject
{
    public string Street { get; } = street;
    public string City { get; } = city;
    public string PostalCode { get; } = postalCode;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return PostalCode;
    }
}
