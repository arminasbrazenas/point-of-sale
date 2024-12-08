namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record UpdateGiftCardDTO
{
    public string? Code { get; init; }
    public decimal? Amount { get; init; }
    public DateTimeOffset? ExpiresAt { get; init; }
}
