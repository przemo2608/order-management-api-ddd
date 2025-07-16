using OrderManagement.Domain.Enums;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities;

public class Order
{
    public OrderId Id { get; private set; }

    public OrderStatus Status { get; private set; }

    private readonly List<Product> _products = [];

    public IReadOnlyList<Product> Products => _products.AsReadOnly();

    private Order(OrderId id)
    {
        Id = id;
        Status = OrderStatus.Draft;
    }

    public static Order CreateNewWithProducts(IEnumerable<Product> products)
    {
        if (products == null || !products.Any())
            throw new ArgumentException("Order must contain at least one product.");

        var order = new Order(OrderId.New());
        foreach (var product in products)
        {
            order.AddProduct(product);
        }
        order.Status = OrderStatus.Draft;
        return order;
    }

    public void AddProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Cannot add products unless order is in Draft status.");

        _products.Add(product);
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        if (newStatus == OrderStatus.Draft)
            throw new InvalidOperationException("Cannot revert to Draft.");

        Status = newStatus;
    }

    public Money GetTotal()
    {
        return _products.Aggregate(Money.Zero, (total, product) => total + product.Price);
    }
}
