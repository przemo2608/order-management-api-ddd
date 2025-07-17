using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects;

public sealed class Customer(string name, string surname) : ValueObject
{
    public string Name { get; } = name ?? throw new DomainException("Name is required");

    public string Surname { get; } = surname ?? throw new DomainException("Surname is required");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
    }

    public void Deconstruct(out string name, out string surname)
    {
        name = Name;
        surname = Surname;
    }
}
