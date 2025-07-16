using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
}
