using OrderManagement.Domain.Aggregates;

namespace OrderManagement.Application.DTOs;

public record OrderDto(
Guid Id,
DateTime CreatedDate,
DateTime? LastModifiedDate,
string Status,
string Street,
string City,
string PostalCode,
decimal TotalPrice,
List<OrderItemDto> Items)
{
    public static OrderDto FromOrder(Order order)
    {
        return new OrderDto(
            Id: order.Id.Value,
            CreatedDate: order.CreatedDate,
            LastModifiedDate: order.LastModifiedDate,
            Status: order.Status.ToString(),
            Street: order.ShippingAddress.Street,
            City: order.ShippingAddress.City,
            PostalCode: order.ShippingAddress.PostalCode,
            TotalPrice: order.TotalPrice.Value,
            Items: order.Items.Select(OrderItemDto.FromOrderItem).ToList()
        );
    }
}
