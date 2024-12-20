using PointOfSale.Models.OrderManagement.Interfaces;
using PointOfSale.Models.Shared.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderServiceCharge : EntityBase<int>, IServiceCharge
{
    public int OrderId { get; set; }
    public required string Name { get; set; }
    public required PricingStrategy PricingStrategy { get; set; }
    public required decimal Amount { get; set; }
    public required decimal AppliedAmount { get; set; }
}
