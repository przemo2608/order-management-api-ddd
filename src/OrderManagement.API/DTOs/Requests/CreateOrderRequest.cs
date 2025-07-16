namespace OrderManagement.API.DTOs.Requests
{
    public record CreateOrderRequest(
    string Street,
    string City,
    string PostalCode);
}
