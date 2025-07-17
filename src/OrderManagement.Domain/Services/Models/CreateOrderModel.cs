namespace OrderManagement.Domain.Services.Models;

public record CreateOrderModel(
    string Street,
    string City,
    string PostalCode,
    string CustomerName,
    string CustomerSurname,
    Guid ProductId,
    int Quantity);
