using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        => await productRepository
            .GetAllAsync()
            .ConfigureAwait(false);
}
