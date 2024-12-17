using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.PaymentManagement.Entities;

public class PaymentRefund : EntityBase<int>
{
    public required string ChargeId { get; set; }
    public required decimal Amount { get; set; }
    public required RefundStatus Status { get; set; }
    public required PaymentMethod Method { get; set; }
    public required string PaymentIntentId { get; set; }
    public required string Reason { get; set; }
}
