namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record GiftCardDTO
{
    public required int Id { get; init; }
    public required string Code { get; init; }
    public required decimal Amount { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public required DateTimeOffset? UsedAt { get; init; }
    public required int BusinessId {get; init;}
}
