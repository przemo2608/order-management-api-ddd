using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs.Responses;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Products.Queries.GetAllProducts;

namespace OrderManagement.API.Endpoints.Products;

public static class GetAllProductsEndpoint
{
    public static async Task<IResult> Handle([FromServices] IMediator mediator)
    {
        var query = new GetAllProductsQuery();

        var productDtos = await mediator.Send(query);

        return Results.Ok(productDtos.Select(MapToResponse));
    }

    private static ProductResponse MapToResponse(ProductDto dto)
    {
        return new ProductResponse(
            Id: dto.Id,
            Name: dto.Name,
            Description: dto.Description,
            Price: dto.Price
        );
    }
}
