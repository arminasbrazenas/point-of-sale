using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateServiceChargeDTO
{
    public required string Name { get; init; }
    public required PricingStrategy PricingStrategy { get; init; }
    public required decimal Amount { get; init; }
    public required int BusinessId { get; init; }
}
