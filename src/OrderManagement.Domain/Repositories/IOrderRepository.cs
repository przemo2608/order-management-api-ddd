using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(OrderId id);

    Task AddAsync(Order order);

    Task UpdateAsync(Order order);
}
