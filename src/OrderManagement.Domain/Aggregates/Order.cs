using OrderManagement.Domain.Common;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Aggregates;

public class Order(Address shippingAddress) : Entity<OrderId>(OrderId.New())
{
    private readonly List<OrderItem> _items = new();

    public DateTime CreatedDate { get; } = DateTime.UtcNow;
    public DateTime? LastModifiedDate { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public Address ShippingAddress { get; private set; } = shippingAddress;
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

    public void RemoveItem(ProductId productId)
    {
        CheckDraftStatus();

        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            throw new DomainException("Product not found in order");

        _items.Remove(item);
        LastModifiedDate = DateTime.UtcNow;
    }

    public void UpdateShippingAddress(Address newAddress)
    {
        CheckDraftStatus();
        ShippingAddress = newAddress;
        LastModifiedDate = DateTime.UtcNow;
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        // Walidacja przejść statusów
        if (Status == OrderStatus.Paid && newStatus == OrderStatus.Cancelled)
            throw new DomainException("Cannot cancel paid order");

        if (newStatus == OrderStatus.Submitted && !_items.Any())
            throw new DomainException("Cannot submit empty order");

        Status = newStatus;
        LastModifiedDate = DateTime.UtcNow;
    }

    private void CheckDraftStatus()
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Only draft orders can be modified");
    }
}
