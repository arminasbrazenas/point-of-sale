using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderItem : EntityBase<int>
{
    public required string Name { get; set; }
    public required decimal BaseUnitPrice { get; set; }
    public required int Quantity { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int? ProductId { get; set; }
    public required Product? Product { get; set; }
    public required List<OrderItemTax> Taxes { get; set; }
    public required List<OrderItemModifier> Modifiers { get; set; }
}
