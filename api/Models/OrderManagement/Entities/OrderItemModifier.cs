using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderItemModifier : EntityBase<int>
{
    public int OrderItemId { get; set; }
    public int? ModifierId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}
