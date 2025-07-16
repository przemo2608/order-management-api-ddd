namespace OrderManagement.API.DTOs.Responses;

public record OrderItemResponse(
Guid ProductId,
string ProductName,
decimal UnitPrice,
int Quantity,
decimal TotalPrice);
