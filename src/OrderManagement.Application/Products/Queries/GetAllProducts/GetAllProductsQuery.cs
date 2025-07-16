using MediatR;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
