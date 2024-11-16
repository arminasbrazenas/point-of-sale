using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Order : EntityBase<int>
{
    public required OrderStatus Status { get; set; }
    public required List<OrderItem> Items { get; set; }
}
