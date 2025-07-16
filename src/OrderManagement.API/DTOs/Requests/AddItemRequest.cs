namespace OrderManagement.API.DTOs.Requests;

public record AddItemRequest(
Guid ProductId,
int Quantity);
