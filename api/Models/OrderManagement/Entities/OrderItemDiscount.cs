using PointOfSale.Models.Shared.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderItemDiscount : EntityBase<int>
{
    public int OrderItemId { get; set; }
    public required decimal Amount { get; set; }
    public required PricingStrategy PricingStrategy { get; set; }
    public required decimal AppliedUnitAmount { get; set; }
}