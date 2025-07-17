namespace OrderManagement.Domain.Common
{
    public abstract class AggregateRoot<TId>(TId id)
    {
        public TId Id { get; protected set; } = id;
    }
}
