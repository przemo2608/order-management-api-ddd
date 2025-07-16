using MediatR;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Orders.Queries.GetOrderDetails;

public record GetOrderDetailsQuery(Guid OrderId) : IRequest<OrderDto>;
