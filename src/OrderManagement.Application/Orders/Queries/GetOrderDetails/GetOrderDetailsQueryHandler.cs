using MediatR;
using OrderManagement.Application.DTOs;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQueryHandler(IOrderService orderService) : IRequestHandler<GetOrderDetailsQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
    {
        var order = await orderService.GetAsync(query.OrderId, cancellationToken);

        return OrderDto.FromOrder(order);
    }
}
