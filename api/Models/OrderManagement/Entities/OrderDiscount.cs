using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.Shared.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderDiscount : EntityBase<int>
{
    public int OrderId { get; set; }
    public required decimal Amount { get; set; }
    public required PricingStrategy PricingStrategy { get; set; }
    public required decimal AppliedAmount { get; set; }
    public required OrderDiscountType Type { get; set; }
}
