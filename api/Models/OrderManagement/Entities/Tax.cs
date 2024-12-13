using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Tax : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal Rate { get; set; }
    public List<Product> Products { get; set; } = null!;
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
}
