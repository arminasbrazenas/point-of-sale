using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateOrderDiscountDTO
{
    public required decimal Amount { get; init; }
    public required PricingStrategy PricingStrategy { get; init; }
}
