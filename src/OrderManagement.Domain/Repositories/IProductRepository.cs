using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Repositories;

public interface IProductRepository
{
    Task InitializeAsync(IEnumerable<Product> products);

    Task<IReadOnlyList<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(ProductId id);
}
