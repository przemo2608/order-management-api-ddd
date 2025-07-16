namespace OrderManagement.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be >= 0");

        Amount = amount;
    }

    public static Money Zero => new(0);

    public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount);

    public static Money operator *(Money m, int qty) => new(m.Amount * qty);

    public override bool Equals(object? obj) =>
        obj is Money other && Amount == other.Amount;

    public override int GetHashCode() => Amount.GetHashCode();

    public override string ToString() => $"{Amount:0.00}";
}
