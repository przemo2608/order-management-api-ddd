using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public sealed class Address(string street, string city, PostalCode postalCode) : ValueObject
{
    public string Street { get; } = street ?? throw new DomainException("Street is required");

    public string City { get; } = city ?? throw new DomainException("City is required");

    public PostalCode PostalCode { get; } = postalCode ?? throw new DomainException("Postal code is required");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return PostalCode;
    }

    public void Deconstruct(out string street, out string city, out PostalCode postalCode)
    {
        street = Street;
        city = City;
        postalCode = PostalCode;
    }
}
