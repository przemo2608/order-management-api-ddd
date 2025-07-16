using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Application.DTOs;

public record OrderItemDto(
Guid ProductId,
string ProductName,
decimal UnitPrice,
int Quantity,
decimal TotalPrice)
{
    public static OrderItemDto FromOrderItem(OrderItem item)
    {
        return new OrderItemDto(
            ProductId: item.ProductId.Value,
            ProductName: item.ProductName.Value,
            UnitPrice: item.UnitPrice.Value,
            Quantity: item.Quantity.Value,
            TotalPrice: item.TotalPrice.Value
        );
    }
}
