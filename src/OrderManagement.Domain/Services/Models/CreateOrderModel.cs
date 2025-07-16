namespace OrderManagement.Domain.Services.Models;

public record CreateOrderModel(
    string Street,
    string City,
    string PostalCode);
