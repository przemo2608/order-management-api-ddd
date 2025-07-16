namespace OrderManagement.Domain.ValueObjects;

public sealed class OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId New() => new(Guid.NewGuid());

    public static OrderId FromGuid(Guid id) => new(id);

    public override bool Equals(object? obj) =>
        obj is OrderId other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
