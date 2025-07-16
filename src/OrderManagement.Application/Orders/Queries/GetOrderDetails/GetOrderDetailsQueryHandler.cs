using MediatR;
using OrderManagement.Application.DTOs;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderDetailsQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(new OrderId(request.OrderId));
        return OrderDto.FromOrder(order);
    }
}
