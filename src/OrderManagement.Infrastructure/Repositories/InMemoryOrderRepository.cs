using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.ValueObjects;
using System.Collections.Concurrent;

namespace OrderManagement.Infrastructure.Repositories;

//Symulacja asynchronicznego repozytorium
public class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<OrderId, Order> _orders = new();

    public Task<Order> GetByIdAsync(OrderId id, CancellationToken cancellationToken)
    {
        if (_orders.TryGetValue(id, out var order))
            return Task.FromResult(order);

        throw new KeyNotFoundException($"Order with ID {id} not found");
    }

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        if (!_orders.TryAdd(order.Id, order))
            throw new InvalidOperationException($"Order with ID {order.Id} already exists");

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        if (!_orders.ContainsKey(order.Id))
            throw new KeyNotFoundException($"Order with ID {order.Id} not found");

        _orders[order.Id] = order;
        return Task.CompletedTask;
    }
}
