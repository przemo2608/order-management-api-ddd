using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Repositories;

public interface IProductRepository
{
    // Tylko metody do odczytu - produkty nie są modyfikowane
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(ProductId id);
}
