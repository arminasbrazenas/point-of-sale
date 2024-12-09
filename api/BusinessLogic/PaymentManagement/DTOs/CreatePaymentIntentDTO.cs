namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record CreatePaymentIntentDTO
{
    public required int OrderId { get; init; }
    public required decimal PaymentAmount { get; init; }
}
