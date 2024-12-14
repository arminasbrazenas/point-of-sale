using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.PaymentManagement.Entities;

public abstract class Payment : EntityBase<int>
{
    public required int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public required decimal Amount { get; set; }
    public required PaymentStatus Status { get; set; }
    public required PaymentMethod Method { get; set; }
}
