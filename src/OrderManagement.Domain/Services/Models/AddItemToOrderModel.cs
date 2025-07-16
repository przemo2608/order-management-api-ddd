namespace OrderManagement.Domain.Services.Models;

public record AddItemToOrderModel(
    Guid OrderId,
    Guid ProductId,
    int Quantity);
