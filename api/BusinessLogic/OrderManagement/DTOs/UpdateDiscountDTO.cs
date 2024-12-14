using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateDiscountDTO
{
    public decimal? Amount { get; init; }
    public PricingStrategy? PricingStrategy { get; init; }
    public DateTimeOffset? ValidUntil { get; init; }
    public List<int>? AppliesToProductIds { get; init; }
}
