using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Modifier : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
    public List<Product> Products { get; set; } = null!;
    public uint RowVersion { get; set; }
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
}
