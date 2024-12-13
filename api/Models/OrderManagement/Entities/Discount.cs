using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.Shared.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Discount : EntityBase<int>
{
    public required decimal Amount { get; set; }
    public required PricingStrategy PricingStrategy { get; set; }
    public required DateTimeOffset ValidUntil { get; set; }
    public required List<Product> AppliesTo { get; set; }
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public required DiscountTarget Target { get; set; }
}
