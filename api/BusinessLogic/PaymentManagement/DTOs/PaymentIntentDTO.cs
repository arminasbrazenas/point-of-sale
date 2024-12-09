namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record PaymentIntentDTO
{
    public required string PaymentId { get; init; }
    public required string ClientSecret { get; init; }
}
