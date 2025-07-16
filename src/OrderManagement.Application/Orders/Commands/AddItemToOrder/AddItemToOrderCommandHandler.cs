using MediatR;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Services.Models;

namespace OrderManagement.Application.Orders.Commands.AddItemToOrder;

public class AddItemToOrderCommandHandler(IOrderService orderService) : IRequestHandler<AddItemToOrderCommand>
{
    public async Task Handle(AddItemToOrderCommand command, CancellationToken cancellationToken)
    {
        var model = new AddItemToOrderModel(command.OrderId, command.ProductId, command.Quantity);

        await orderService.AddItemToOrderAsync(model, cancellationToken).ConfigureAwait(false);
    }
}
