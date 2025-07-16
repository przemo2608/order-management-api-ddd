using MediatR;
using OrderManagement.Application.DTOs;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync();
        return products.Select(ProductDto.FromProduct);
    }
}
