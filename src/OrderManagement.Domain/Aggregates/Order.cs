using OrderManagement.Domain.Common;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Aggregates;

public class Order(Address shippingAddress, Customer customer, OrderItem item) : AggregateRoot<OrderId>(OrderId.New())
{
    private readonly List<OrderItem> _items = [item];

    public DateTime CreatedDate { get; } = DateTime.UtcNow;

    public DateTime? LastModifiedDate { get; private set; }

    public OrderStatus Status { get; private set; } = OrderStatus.Draft;

    public Address ShippingAddress { get; private set; } = shippingAddress;

    public Customer Customer { get; private set; } = customer;

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Price TotalPrice => new(Items.Sum(item => item.TotalPrice.Value));

    public void AddItem(Product product, Quantity quantity)
    {
        CheckDraftStatus();

        var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
        if (existingItem != null)
        {
            var newQuantity = new Quantity(existingItem.Quantity.Value + quantity.Value);
            _items.Remove(existingItem);
            _items.Add(new OrderItem(product, newQuantity));
        }
        else
        {
            _items.Add(new OrderItem(product, quantity));
        }

        LastModifiedDate = DateTime.UtcNow;
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Paid || Status == OrderStatus.Cancelled)
            throw new DomainException("Cannot change paid or cancelled order");

        Status = newStatus;
        LastModifiedDate = DateTime.UtcNow;
    }

    private void CheckDraftStatus()
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Only draft orders can be modified");
    }
}
