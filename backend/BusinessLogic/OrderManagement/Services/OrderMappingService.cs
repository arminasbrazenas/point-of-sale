using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class OrderMappingService : IOrderMappingService
{
    public OrderMinimalDTO MapToOrderMinimalDTO(Order order)
    {
        return new OrderMinimalDTO
        {
            Id = order.Id,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
        };
    }

    public PagedResponseDTO<OrderMinimalDTO> MapToPagedOrderMinimalDTO(
        List<Order> orders,
        PaginationFilter paginationFilter
    )
    {
        return new PagedResponseDTO<OrderMinimalDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            Items = orders.Select(MapToOrderMinimalDTO).ToList(),
        };
    }

    public OrderDTO MapToOrderDTO(Order order)
    {
        var orderItems = order.Items.Select(MapToOrderItemDTO).ToList();
        var totalPrice = orderItems.Sum(i => i.TotalPrice);

        return new OrderDTO
        {
            Id = order.Id,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            OrderItems = orderItems,
            TotalPrice = totalPrice.ToRoundedPrice(),
        };
    }

    private static OrderItemDTO MapToOrderItemDTO(OrderItem orderItem)
    {
        var subtotal = orderItem.BaseUnitPrice * orderItem.Quantity;
        var taxRates = orderItem.Taxes.Select(t => t.TaxRate);
        var totalPrice = subtotal + PriceUtility.CalculateTotalTax(subtotal, taxRates);

        return new OrderItemDTO
        {
            Id = orderItem.Id,
            Name = orderItem.Name,
            Quantity = orderItem.Quantity,
            TotalPrice = totalPrice.ToRoundedPrice(),
        };
    }
}
