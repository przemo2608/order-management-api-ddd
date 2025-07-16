using MediatR;
using OrderManagement.API.DTOs.Requests;
using OrderManagement.Application.Orders.Commands.UpdateOrderStatus;

namespace OrderManagement.API.Endpoints.Orders;

public static class UpdateOrderStatusEndpoint
{
    public static async Task<IResult> Handle(
        Guid orderId,
        UpdateStatusRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateOrderStatusCommand(
            OrderId: orderId,
            Status: request.Status);

        await mediator.Send(command, cancellationToken);

        return Results.Ok();
    }
}
