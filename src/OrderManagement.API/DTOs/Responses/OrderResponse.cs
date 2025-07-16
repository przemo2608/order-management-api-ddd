namespace OrderManagement.API.DTOs.Responses;

public record OrderResponse(
Guid Id,
DateTime CreatedDate,
DateTime? LastModifiedDate,
string Status,
string Street,
string City,
string PostalCode,
decimal TotalPrice,
List<OrderItemResponse> Items);
