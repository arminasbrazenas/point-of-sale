using PointOfSale.Models.Shared.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Discount : EntityBase<int>
{
    public required decimal Amount { get; set; }
    public required PricingStrategy PricingStrategy { get; set; }
    public required DateTimeOffset ValidUntil { get; set; }
    public required List<Product> AppliesTo { get; set; }
}