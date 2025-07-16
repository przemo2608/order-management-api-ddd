namespace OrderManagement.Domain.ValueObjects;

public sealed class ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId New() => new(Guid.NewGuid());

    public static ProductId FromGuid(Guid id) => new(id);

    public override bool Equals(object? obj) =>
        obj is ProductId other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
