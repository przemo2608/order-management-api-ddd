using MediatR;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Services.Models;

namespace OrderManagement.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler(IOrderService orderService) : IRequestHandler<UpdateOrderStatusCommand>
{
    public async Task Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var model = new UpdateOrderStatusModel(command.OrderId, command.Status);

        await orderService.UpdateOrderStatusAsync(model, cancellationToken).ConfigureAwait(false);
    }
}
