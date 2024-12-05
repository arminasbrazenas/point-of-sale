using PointOfSale.Models.PaymentProcessing.Enums;

namespace PointOfSale.BusinessLogic.PaymentProcessing.DTOs
{
    public class PaymentDetail
    {
        public required PaymentMethod PaymentMethod { get; set; }
        public required decimal Amount { get; set; }
        public string? TransactionId { get; set; }
    }
}