﻿using MediatR;
using OrderManagement.API.DTOs.Responses;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Orders.Queries.GetOrderDetails;

namespace OrderManagement.API.Endpoints.Orders;

public static class GetOrderEndpoint
{
    public static async Task<IResult> Handle(
        Guid orderId,
        IMediator mediator)
    {
        var query = new GetOrderDetailsQuery(orderId);

        var orderDto = await mediator.Send(query);

        return Results.Ok(MapToResponse(orderDto));
    }

    private static OrderResponse MapToResponse(OrderDto dto)
    {
        return new OrderResponse(
            Id: dto.Id,
            CreatedDate: dto.CreatedDate,
            LastModifiedDate: dto.LastModifiedDate,
            Status: dto.Status,
            CustomerName: dto.CustomerName,
            CustomerSurname: dto.CustomerSurname,
            Street: dto.Street,
            City: dto.City,
            PostalCode: dto.PostalCode,
            TotalPrice: dto.TotalPrice,
            Items: dto.Items.Select(MapItemToResponse).ToList()
        );
    }

    private static OrderItemResponse MapItemToResponse(OrderItemDto itemDto)
    {
        return new OrderItemResponse(
            ProductId: itemDto.ProductId,
            ProductName: itemDto.ProductName,
            UnitPrice: itemDto.UnitPrice,
            Quantity: itemDto.Quantity,
            TotalPrice: itemDto.TotalPrice
        );
    }
}
