using PointOfSale.Models.PaymentProcessing.Enums;

namespace PointOfSale.BusinessLogic.PaymentProcessing.DTOs
{
    public class CreatePaymentRequest
    {
        public required List<PaymentDetail> Payments { get; set; } = new();
        public required decimal TipAmount { get; set; }
    }
}