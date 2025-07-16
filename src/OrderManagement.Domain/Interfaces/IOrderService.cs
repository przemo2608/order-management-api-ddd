using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Services.Models;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderModel model, CancellationToken cancellationToken);

        Task AddItemToOrderAsync(AddItemToOrderModel model, CancellationToken cancellationToken);

        Task UpdateOrderStatusAsync(UpdateOrderStatusModel model, CancellationToken cancellationToken);
    }
}
