using MediatR;

namespace OrderManagement.Application.Orders.Commands.AddItemToOrder;

public record AddItemToOrderCommand(
Guid OrderId,
Guid ProductId,
int Quantity) : IRequest;
