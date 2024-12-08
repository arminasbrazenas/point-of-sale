namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record CreatePaymentDTO
{
    public required int OrderId { get; init; }
    public required decimal PaymentAmount { get; init; }
}
