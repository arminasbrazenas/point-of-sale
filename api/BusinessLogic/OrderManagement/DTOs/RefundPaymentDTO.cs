namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record RefundPaymentDTO
{
    public required string PaymentIntentId { get; init; }
    public required decimal Amount { get; init; }
}
