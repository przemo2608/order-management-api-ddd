using MediatR;
using OrderManagement.API.DTOs.Requests;
using OrderManagement.Application.Orders.Commands.CreateOrder;

namespace OrderManagement.API.Endpoints.Orders;

public static class CreateOrderEndpoint
{
    public static async Task<IResult> Handle(
        CreateOrderRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(
            request.Street,
            request.City,
            request.PostalCode,
            request.CustomerName,
            request.CustomerSurname,
            request.ProductId,
            request.Quantity);

        var orderId = await mediator.Send(command, cancellationToken);

        return Results.Created($"/orders/{orderId}", new { Id = orderId });
    }
}
