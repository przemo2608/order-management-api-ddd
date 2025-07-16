using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public PostalCode PostalCode { get; }

    public Address(string street, string city, PostalCode postalCode)
    {
        Street = street ?? throw new DomainException("Street is required");
        City = city ?? throw new DomainException("City is required");
        PostalCode = postalCode ?? throw new DomainException("Postal code is required");
    }

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
