using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.ValueObjects;
using System.Collections.Concurrent;

namespace OrderManagement.Infrastructure.Repositories;

public class InMemoryProductRepository() : IProductRepository
{
    private readonly ConcurrentDictionary<ProductId, Product> _products = new();

    public Task<IReadOnlyList<Product>> GetAllAsync()
    {
        var products = _products.Values.ToList().AsReadOnly();
        return Task.FromResult<IReadOnlyList<Product>>(products);
    }

    public Task<Product> GetByIdAsync(ProductId id)
    {
        if (_products.TryGetValue(id, out var product))
            return Task.FromResult(product);

        throw new KeyNotFoundException($"Product with ID {id} not found");
    }

    public Task InitializeAsync(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            if (!_products.TryAdd(product.Id, product))
                throw new InvalidOperationException($"Product with ID {product.Id} already exists");
        }
        return Task.CompletedTask;
    }
}
