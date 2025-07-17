using OrderManagement.Domain.Aggregates;

namespace OrderManagement.Application.DTOs;

public record OrderDto(
Guid Id,
DateTime CreatedDate,
DateTime? LastModifiedDate,
string Status,
string CustomerName,
string CustomerSurname,
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
            CustomerName: order.Customer.Name,
            CustomerSurname: order.Customer.Surname,
            Street: order.ShippingAddress.Street,
            City: order.ShippingAddress.City,
            PostalCode: order.ShippingAddress.PostalCode,
            TotalPrice: order.TotalPrice.Value,
            Items: order.Items.Select(OrderItemDto.FromOrderItem).ToList()
        );
    }
}
