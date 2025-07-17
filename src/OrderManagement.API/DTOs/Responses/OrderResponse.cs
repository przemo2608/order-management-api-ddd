namespace OrderManagement.API.DTOs.Responses;

public record OrderResponse(
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
List<OrderItemResponse> Items);
