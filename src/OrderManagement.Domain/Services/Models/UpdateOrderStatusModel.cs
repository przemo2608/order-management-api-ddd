namespace OrderManagement.Domain.Services.Models;

public record UpdateOrderStatusModel(
    Guid orderId,
    string status);
