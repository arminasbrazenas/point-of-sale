namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record TipDTO
{
    public required int Id { get; init; }
    public required decimal Amount { get; init; }
}
