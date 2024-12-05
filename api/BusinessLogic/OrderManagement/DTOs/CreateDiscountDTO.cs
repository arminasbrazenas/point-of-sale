using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateDiscountDTO
{
    public required decimal Amount { get; init; }
    public required PricingStrategy PricingStrategy { get; init; }
    public required DateTimeOffset ValidUntil { get; init; }
    public required List<int> AppliesToProductIds { get; init; }
}