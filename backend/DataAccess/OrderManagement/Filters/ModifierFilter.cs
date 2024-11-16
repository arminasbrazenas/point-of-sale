namespace PointOfSale.DataAccess.OrderManagement.Filters;

public record ModifierFilter
{
    public int? CompatibleWithProductById { get; init; }
}
