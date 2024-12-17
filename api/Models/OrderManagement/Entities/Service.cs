using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Service : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required TimeSpan Duration { get; set; }
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public required List<ApplicationUser> ProvidedByEmployees { get; set; }
}
