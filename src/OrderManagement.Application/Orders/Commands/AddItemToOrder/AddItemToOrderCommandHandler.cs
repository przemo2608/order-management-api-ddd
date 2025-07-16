using MediatR;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Application.Orders.Commands.AddItemToOrder;

public class AddItemToOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductRepository productRepository) : IRequestHandler<AddItemToOrderCommand>
{
    public async Task Handle(AddItemToOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(new OrderId(request.OrderId));
        var product = await productRepository.GetByIdAsync(new ProductId(request.ProductId));

        if (product is null)
            throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");

        order.AddItem(product, new Quantity(request.Quantity));
        await orderRepository.UpdateAsync(order);
    }
}
