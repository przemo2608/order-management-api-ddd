using MediatR;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Services.Models;

namespace OrderManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderService orderService) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var model = new CreateOrderModel(command.Street, command.City, command.PostalCode);

        var createdOrder = await orderService
            .CreateOrderAsync(model, cancellationToken)
            .ConfigureAwait(false);

        return createdOrder.Id.Value;
    }
}
