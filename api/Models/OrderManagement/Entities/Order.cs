using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Order : EntityBase<int>
{
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public required OrderStatus Status { get; set; }
    public int? ReservationId { get; set; }
    public Reservation? Reservation { get; set; }
    public required List<OrderItem> Items { get; set; }
    public required List<OrderServiceCharge> ServiceCharges { get; set; }
    public required List<OrderDiscount> Discounts { get; set; }
}
