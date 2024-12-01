using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateServiceChargeDTO
{
    public string? Name { get; init; }
    public PricingStrategy? PricingStrategy { get; init; }
    public decimal? Amount { get; init; }
}
