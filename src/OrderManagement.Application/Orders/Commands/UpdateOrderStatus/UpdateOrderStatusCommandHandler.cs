using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderStatusCommand>
{
    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(new OrderId(request.OrderId));
        var status = Enum.Parse<OrderStatus>(request.Status);

        order.ChangeStatus(status);
        await orderRepository.UpdateAsync(order);
    }
}
