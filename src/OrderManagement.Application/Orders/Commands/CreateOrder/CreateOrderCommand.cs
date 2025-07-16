using MediatR;

namespace OrderManagement.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
string Street,
string City,
string PostalCode) : IRequest<Guid>;
