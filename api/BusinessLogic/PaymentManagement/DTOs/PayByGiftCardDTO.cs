namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record PayByGiftCardDTO
{
    public required int OrderId { get; init; }
    public required string GiftCardCode { get; init; }
}
