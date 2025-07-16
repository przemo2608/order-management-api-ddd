using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs.Requests;
using OrderManagement.Application.Orders.Commands.UpdateOrderStatus;

namespace OrderManagement.API.Endpoints.Orders
{
    public static class UpdateOrderStatusEndpoint
    {
        public static async Task<IResult> Handle(
            [FromRoute] Guid id,
            [FromBody] UpdateStatusRequest request,
            [FromServices] IMediator mediator)
        {
            var command = new UpdateOrderStatusCommand(
                OrderId: id,
                Status: request.Status);

            await mediator.Send(command);
            return Results.Ok();
        }
    }
}
