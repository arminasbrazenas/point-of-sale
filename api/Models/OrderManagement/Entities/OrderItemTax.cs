using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderItemTax : EntityBase<int>
{
    public int OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; } = null!;
    public required string Name { get; set; }
    public required decimal Rate { get; set; }
    public required decimal AppliedUnitAmount { get; set; }
}
