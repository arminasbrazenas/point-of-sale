using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class OrderMappingService : IOrderMappingService
{
    private readonly IReservationMappingService _reservationMappingService;

    public OrderMappingService(IReservationMappingService reservationMappingService)
    {
        _reservationMappingService = reservationMappingService;
    }

    public OrderMinimalDTO MapToOrderMinimalDTO(Order order)
    {
        return new OrderMinimalDTO
        {
            Id = order.Id,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            BusinessId = order.BusinessId,
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
        var orderDiscounts = order.Discounts.Select(MapToOrderDiscountDTO).ToList();
        var totalPrice =
            orderItems.Sum(i => i.TotalPrice)
            - orderDiscounts.Sum(d => d.AppliedAmount)
            + serviceCharges.Sum(c => c.AppliedAmount);

        if (order.Reservation is not null)
        {
            totalPrice += order.Reservation.Price;
        }

        return new OrderDTO
        {
            Id = order.Id,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            OrderItems = orderItems,
            TotalPrice = totalPrice,
            ServiceCharges = serviceCharges,
            Discounts = orderDiscounts,
            BusinessId = order.BusinessId,
            CreatedBy = $"{order.CreatedBy.FirstName} {order.CreatedBy.LastName}",
            Reservation = order.Reservation is null
                ? null
                : _reservationMappingService.MapToReservationDTO(order.Reservation),
        };
    }

    public OrderReceiptDTO MapToOrderReceiptDTO(Order order)
    {
        var orderDTO = MapToOrderDTO(order);

        return new OrderReceiptDTO
        {
            TotalPrice = orderDTO.TotalPrice,
            OrderItems = orderDTO.OrderItems,
            Discounts = order.Discounts.Select(MapToOrderDiscountDTO).ToList(),
            ServiceCharges = orderDTO.ServiceCharges,
            Reservation = order.Reservation is null
                ? null
                : _reservationMappingService.MapToReservationDTO(order.Reservation),
        };
    }

    private static OrderItemDTO MapToOrderItemDTO(OrderItem orderItem)
    {
        var baseUnitPrice = orderItem.BaseUnitPrice;
        var modifiersPrice = orderItem.Modifiers.Sum(m => m.Price);
        var unitPrice = baseUnitPrice + modifiersPrice;
        var discountsTotal = orderItem.Discounts.Sum(d => d.AppliedAmount);
        var taxTotal = orderItem.Taxes.Sum(t => t.AppliedAmount);
        var totalPrice = unitPrice * orderItem.Quantity - discountsTotal + taxTotal;

        return new OrderItemDTO
        {
            Id = orderItem.Id,
            ProductId = orderItem.ProductId,
            Name = orderItem.Name,
            Quantity = orderItem.Quantity,
            UnitPrice = unitPrice,
            TaxTotal = taxTotal,
            DiscountsTotal = discountsTotal,
            TotalPrice = totalPrice,
            Modifiers = orderItem.Modifiers.Select(MapToOrderItemModifierDTO).ToList(),
            Discounts = orderItem.Discounts.Select(MapToOrderDiscountDTO).ToList(),
            Taxes = orderItem.Taxes.Select(MapToOrderItemTaxDTO).ToList(),
        };
    }

    private static OrderItemModifierDTO MapToOrderItemModifierDTO(OrderItemModifier modifier)
    {
        return new OrderItemModifierDTO
        {
            ModifierId = modifier.ModifierId,
            Name = modifier.Name,
            Price = modifier.Price,
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
            AppliedBy = $"{serviceCharge.ModifiedBy.FirstName} {serviceCharge.ModifiedBy.LastName}",
        };
    }

    private static OrderDiscountDTO MapToOrderDiscountDTO(OrderDiscount orderDiscount)
    {
        return new OrderDiscountDTO
        {
            Id = orderDiscount.OrderId,
            Amount = orderDiscount.Amount,
            PricingStrategy = orderDiscount.PricingStrategy,
            AppliedAmount = orderDiscount.AppliedAmount,
            Type = orderDiscount.Type,
            AppliedBy =
                orderDiscount.Type == OrderDiscountType.Predefined
                    ? "System"
                    : $"{orderDiscount.ModifiedBy.FirstName} {orderDiscount.ModifiedBy.LastName}",
        };
    }

    private static OrderDiscountDTO MapToOrderDiscountDTO(OrderItemDiscount orderItemDiscount)
    {
        return new OrderDiscountDTO
        {
            Id = orderItemDiscount.Id,
            Amount = orderItemDiscount.Amount,
            PricingStrategy = orderItemDiscount.PricingStrategy,
            AppliedAmount = orderItemDiscount.AppliedAmount,
            Type = orderItemDiscount.Type,
            AppliedBy =
                orderItemDiscount.Type == OrderDiscountType.Predefined
                    ? "System"
                    : $"{orderItemDiscount.ModifiedBy.FirstName} {orderItemDiscount.ModifiedBy.LastName}",
        };
    }

    private static OrderItemTaxDTO MapToOrderItemTaxDTO(OrderItemTax orderItemTax)
    {
        return new OrderItemTaxDTO
        {
            AppliedAmount = orderItemTax.AppliedAmount,
            Name = orderItemTax.Name,
            RatePercentage = (orderItemTax.Rate * 100m).ToRoundedPrice(),
        };
    }
}
