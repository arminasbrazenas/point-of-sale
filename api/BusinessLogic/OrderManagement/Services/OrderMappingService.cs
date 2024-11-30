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
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<OrderMinimalDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
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
            TotalPrice = totalPrice,
        };
    }

    public OrderReceiptDTO MapToOrderReceiptDTO(Order order)
    {
        var orderDTO = MapToOrderDTO(order);
        var taxTotal = orderDTO.OrderItems.Sum(i => i.TaxTotal);

        return new OrderReceiptDTO
        {
            TotalPrice = orderDTO.TotalPrice,
            OrderItems = orderDTO.OrderItems,
            TaxTotal = taxTotal,
        };
    }

    private static OrderItemDTO MapToOrderItemDTO(OrderItem orderItem)
    {
        var modifiersPrice = orderItem.Modifiers.Sum(i => i.Price).ToRoundedPrice();
        var preTaxUnitPrice = (orderItem.BaseUnitPrice + modifiersPrice).ToRoundedPrice();
        var taxRates = orderItem.Taxes.Select(t => t.Rate).ToList();
        var unitTaxTotal = PriceUtility.CalculateTotalTax(preTaxUnitPrice, taxRates).ToRoundedPrice();
        var unitPrice = (orderItem.BaseUnitPrice + unitTaxTotal).ToRoundedPrice();
        var taxTotal = unitTaxTotal * orderItem.Quantity;
        var totalPrice = unitPrice * orderItem.Quantity;

        return new OrderItemDTO
        {
            Id = orderItem.Id,
            ProductId = orderItem.ProductId,
            Name = orderItem.Name,
            Quantity = orderItem.Quantity,
            UnitPrice = unitPrice,
            TotalPrice = totalPrice,
            TaxTotal = taxTotal,
            Modifiers = orderItem.Modifiers.Select(MapToOrderItemModifierDTO).ToList(),
        };
    }

    private static OrderItemModifierDTO MapToOrderItemModifierDTO(OrderItemModifier modifier)
    {
        return new OrderItemModifierDTO { Name = modifier.Name, Price = modifier.Price };
    }
}
