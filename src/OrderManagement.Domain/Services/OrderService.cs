using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services.Models;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IProductRepository productRepository) : IOrderService
{
    public async Task<Order> CreateOrderAsync(CreateOrderModel model, CancellationToken cancellationToken)
    {
        var product = await productRepository
            .GetByIdAsync(model.ProductId)
            .ConfigureAwait(false);

        var order = new Order(
            new(model.Street, model.City, model.PostalCode),
            new(model.CustomerName, model.CustomerSurname),
            new(product!, model.Quantity));

        await orderRepository.AddAsync(order, cancellationToken).ConfigureAwait(false);

        return order;
    }

    public async Task AddItemToOrderAsync(AddItemToOrderModel model, CancellationToken cancellationToken)
    {
        var order = await orderRepository
            .GetByIdAsync(model.OrderId, cancellationToken)
            .ConfigureAwait(false);

        var product = await productRepository
            .GetByIdAsync(model.ProductId)
            .ConfigureAwait(false);

        order!.AddItem(product!, new Quantity(model.Quantity));

        await orderRepository.UpdateAsync(order, cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateOrderStatusAsync(UpdateOrderStatusModel model, CancellationToken cancellationToken)
    {
        var order = await orderRepository
            .GetByIdAsync(model.orderId, cancellationToken)
            .ConfigureAwait(false);

        var status = Enum.Parse<OrderStatus>(model.status);

        order!.ChangeStatus(status);

        await orderRepository.UpdateAsync(order, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await orderRepository
            .GetByIdAsync(orderId, cancellationToken)
            .ConfigureAwait(false);

        return order!;
    }
}
