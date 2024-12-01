using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Product : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
    public required List<Tax> Taxes { get; set; }
    public required List<Modifier> Modifiers { get; set; }
    public required List<Discount> Discounts { get; set; }
    public uint RowVersion { get; set; }
}
