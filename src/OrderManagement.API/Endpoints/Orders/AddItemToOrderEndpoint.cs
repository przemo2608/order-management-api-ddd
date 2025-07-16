using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs.Requests;
using OrderManagement.Application.Orders.Commands.AddItemToOrder;

namespace OrderManagement.API.Endpoints.Orders
{
    public static class AddItemToOrderEndpoint
    {
        public static async Task<IResult> Handle(
            [FromRoute] Guid id,
            [FromBody] AddItemRequest request,
            [FromServices] IMediator mediator)
        {
            var command = new AddItemToOrderCommand(
                OrderId: id,
                ProductId: request.ProductId,
                Quantity: request.Quantity);

            await mediator.Send(command);
            return Results.Ok();
        }
    }
}
