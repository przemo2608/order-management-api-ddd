namespace OrderManagement.API.DTOs.Responses;

public record ProductResponse(
Guid Id,
string Name,
string Description,
decimal Price);
