using Stripe;

namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs
{
    public class RefundResponseDTO
    {
        public required string RefundId { get; set; }
        public required string Status { get; set; }
        public long AmountRefunded { get; set; }
        public required Charge ChargeId { get; set; }
        public required PaymentIntent PaymentIntentId { get; set; }
        public required string Reason { get; set; }
    }
}
