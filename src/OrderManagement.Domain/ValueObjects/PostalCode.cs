using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OrderManagement.Domain.ValueObjects
{
    public sealed class PostalCode : ValueObject
    {
        public string Value { get; }

        public PostalCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Postal code cannot be empty");

            // Walidacja formatu
            if (!Regex.IsMatch(value, @"^\d{2}-\d{3}$"))
                throw new DomainException("Invalid postal code format");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(PostalCode postalCode) => postalCode.Value;
        public static implicit operator PostalCode(string value) => new(value);

        public override string ToString() => Value;
    }
}
