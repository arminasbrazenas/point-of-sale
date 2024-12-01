using PointOfSale.Models.Shared.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderServiceCharge : EntityBase<int>
{
    public int OrderId { get; set; }
    public required string Name { get; set; }
    public required PricingStrategy PricingStrategy { get; set; }
    public required decimal Amount { get; set; }
}
