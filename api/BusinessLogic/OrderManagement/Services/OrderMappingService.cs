using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
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
        var serviceCharges = order.ServiceCharges.Select(MapToOrderServiceChargeDTO).ToList();
        var totalPrice = orderItems.Sum(i => i.TotalPrice) + serviceCharges.Sum(c => c.AppliedAmount);

        return new OrderDTO
        {
            Id = order.Id,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            OrderItems = orderItems,
            TotalPrice = totalPrice,
            ServiceCharges = serviceCharges,
        };
    }

    public OrderReceiptDTO MapToOrderReceiptDTO(Order order)
    {
        var orderDTO = MapToOrderDTO(order);
        var taxTotal = orderDTO.OrderItems.Sum(i => i.TaxTotal);
        var serviceChargeTotal = orderDTO.ServiceCharges.Sum(c => c.AppliedAmount);

        return new OrderReceiptDTO
        {
            TotalPrice = orderDTO.TotalPrice,
            OrderItems = orderDTO.OrderItems,
            TaxTotal = taxTotal,
            ServiceCharges = orderDTO.ServiceCharges,
            ServiceChargeTotal = serviceChargeTotal,
        };
    }

    private static OrderItemDTO MapToOrderItemDTO(OrderItem orderItem)
    {
        var unitDiscount = orderItem.Discounts.Sum(d => d.AppliedUnitAmount);
        var unitModifiersPrice = orderItem.Modifiers.Sum(m => m.GrossPrice);
        var unitTaxTotal = orderItem.Taxes.Sum(t => t.AppliedUnitAmount);
        var unitNetPrice = orderItem.BaseUnitGrossPrice - unitDiscount + unitModifiersPrice + unitTaxTotal;
        var totalPrice = unitNetPrice * orderItem.Quantity;
        var taxTotal = unitTaxTotal * orderItem.Quantity;

        return new OrderItemDTO
        {
            Id = orderItem.Id,
            ProductId = orderItem.ProductId,
            Name = orderItem.Name,
            Quantity = orderItem.Quantity,
            UnitPrice = unitNetPrice,
            TotalPrice = totalPrice,
            TaxTotal = taxTotal,
            Modifiers = orderItem.Modifiers.Select(MapToOrderItemModifierDTO).ToList(),
        };
    }

    private static OrderItemModifierDTO MapToOrderItemModifierDTO(OrderItemModifier modifier)
    {
        return new OrderItemModifierDTO
        {
            ModifierId = modifier.ModifierId,
            Name = modifier.Name,
            Price = modifier.GrossPrice + modifier.TaxTotal,
        };
    }

    private static OrderServiceChargeDTO MapToOrderServiceChargeDTO(OrderServiceCharge serviceCharge)
    {
        return new OrderServiceChargeDTO
        {
            Name = serviceCharge.Name,
            Amount = serviceCharge.Amount,
            PricingStrategy = serviceCharge.PricingStrategy,
            AppliedAmount = serviceCharge.AppliedAmount,
        };
    }
}
