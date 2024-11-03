using PointOfSale.DataAccess.Shared.Entities;

namespace PointOfSale.DataAccess.Order.Entities;

public class Product : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
    public required List<Tax> Taxes { get; set; }
}
