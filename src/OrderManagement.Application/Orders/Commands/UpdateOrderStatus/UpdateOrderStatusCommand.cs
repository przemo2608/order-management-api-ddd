using MediatR;

namespace OrderManagement.Application.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(
Guid OrderId,
string Status) : IRequest;
