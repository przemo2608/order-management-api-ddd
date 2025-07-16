using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs.Requests;
using OrderManagement.Application.Orders.Commands.CreateOrder;

namespace OrderManagement.API.Endpoints.Orders
{
    public static class CreateOrderEndpoint
    {
        public static async Task<IResult> Handle(
            [FromBody] CreateOrderRequest request,
            [FromServices] IMediator mediator)
        {
            var command = new CreateOrderCommand(
                request.Street,
                request.City,
                request.PostalCode);

            var orderId = await mediator.Send(command);
            return Results.Created($"/orders/{orderId}", new { Id = orderId });
        }
    }
}
