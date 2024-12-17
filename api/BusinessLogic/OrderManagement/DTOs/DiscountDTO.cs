using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record DiscountDTO
{
    public required int Id { get; init; }
    public required decimal Amount { get; init; }
    public required PricingStrategy PricingStrategy { get; init; }
    public required DateTimeOffset ValidUntil { get; init; }
    public required List<int>? AppliesToProductIds { get; init; }
    public required DiscountTarget Target { get; set; }
    public required int BusinessId { get; init; }
}
