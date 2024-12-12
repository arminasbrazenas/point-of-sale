using PointOfSale.Models.PaymentProcessing.Enums;

namespace PointOfSale.BusinessLogic.PaymentProcessing.DTOs
{
    public record PaymentDTO
    {
        public required bool IsSplitPayment { get; set; }
        public required PaymentMethod PaymentMethod { get; set; }
        public required PaymentStatus PaymentStatus { get; set; }
        public required decimal PayAmount { get; set; }
        
    }
}
