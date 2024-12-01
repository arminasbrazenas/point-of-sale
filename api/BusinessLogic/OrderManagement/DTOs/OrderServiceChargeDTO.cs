using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderServiceChargeDTO
{
    public required string Name { get; init; }
    public required decimal Amount { get; init; }
    public required PricingStrategy PricingStrategy { get; init; }
}
