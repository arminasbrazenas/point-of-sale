using PointOfSale.Models.Shared.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Entities;

public class ServiceCharge : EntityBase<int>
{
    public required string Name { get; init; }
    public required PricingStrategy PricingStrategy { get; init; }
    public required decimal Amount { get; init; }
}
