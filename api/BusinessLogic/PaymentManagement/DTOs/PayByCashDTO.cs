namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record PayByCashDTO
{
    public required int OrderId { get; init; }
    public required decimal PaymentAmount { get; init; }
}
