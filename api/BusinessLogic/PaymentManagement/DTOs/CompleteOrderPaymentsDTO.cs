namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record CompleteOrderPaymentsDTO
{
    public required int OrderId { get; init; }
}
