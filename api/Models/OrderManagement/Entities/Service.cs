using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Service
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required TimeSpan Duration { get; set; }
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
}