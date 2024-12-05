using PointOfSale.Models.PaymentProcessing.Enums;

namespace PointOfSale.BusinessLogic.PaymentProcessing.DTOs
{
    public record PaymentDTO
    {
		public required int OrderId { get; set; }
		public required decimal TotalPaid { get; set; }
        public required PaymentStatus PaymentStatus { get; set; }
    }
}
