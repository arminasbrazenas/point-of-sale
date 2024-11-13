using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Modifier : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Amount { get; set; }
    public required List<Product> Products { get; set; }
    public uint RowVersion { get; set; }
}