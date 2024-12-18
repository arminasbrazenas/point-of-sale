using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderDiscountDTO
{
    public required int Id { get; init; }
    public required decimal Amount { get; init; }
    public required PricingStrategy PricingStrategy { get; init; }
    public required decimal AppliedAmount { get; init; }
    public required OrderDiscountType Type { get; init; }
    public required string AppliedBy { get; init; }
}
