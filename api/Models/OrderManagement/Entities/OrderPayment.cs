using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.PaymentProcessing.Enums;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.OrderManagement.Entities;

public class OrderPayment : EntityBase<int>
{
    public int OrderId { get; set; }
    public required decimal Amount { get; set; }
    public required decimal TotalPaid { get; set; }
    public required PaymentStatus PaymentStatus { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }
    public Order Order { get; set; } = null!;
}
