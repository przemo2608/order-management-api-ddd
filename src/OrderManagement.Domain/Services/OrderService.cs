using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services.Models;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository) : IOrderService
    {
        public async Task<Order> CreateOrderAsync(CreateOrderModel model, CancellationToken cancellationToken)
        {
            var order = new Order(new(model.Street, model.City, model.PostalCode));
            await orderRepository.AddAsync(order, cancellationToken);
            return order;
        }

        public async Task AddItemToOrderAsync(AddItemToOrderModel model, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(model.OrderId, cancellationToken);
            var product = await productRepository.GetByIdAsync(model.ProductId);

            if (product == null)
                throw new DomainException($"Product with ID {model.ProductId} not found");

            order.AddItem(product, new Quantity(model.Quantity));
            await orderRepository.UpdateAsync(order, cancellationToken);
        }

        public async Task UpdateOrderStatusAsync(UpdateOrderStatusModel model, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(model.orderId, cancellationToken);
            var status = Enum.Parse<OrderStatus>(model.status);
            order.ChangeStatus(status);
            await orderRepository.UpdateAsync(order, cancellationToken);
        }
    }
}
