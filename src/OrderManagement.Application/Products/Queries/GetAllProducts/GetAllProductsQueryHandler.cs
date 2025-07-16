using MediatR;
using OrderManagement.Application.DTOs;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductService productService) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productService
            .GetAllAsync(cancellationToken)
            .ConfigureAwait(false);

        return products.Select(ProductDto.FromProduct);
    }
}
