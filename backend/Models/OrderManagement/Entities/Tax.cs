using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Tax : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal Rate { get; set; }
    public required List<Product> Products { get; set; }
}
