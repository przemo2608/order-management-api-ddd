using MediatR;
using OrderManagement.API.DTOs.Requests;
using OrderManagement.Application.Orders.Commands.AddItemToOrder;

namespace OrderManagement.API.Endpoints.Orders;

public static class AddItemToOrderEndpoint
{
    public static async Task<IResult> Handle(
        Guid orderId,
        AddItemRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new AddItemToOrderCommand(
            orderId,
            request.ProductId,
            request.Quantity);

        await mediator.Send(command, cancellationToken);

        return Results.Ok();
    }
}
