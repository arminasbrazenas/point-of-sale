namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record GiftCardPaymentDTO : PaymentDTO
{
    public required string GiftCardCode { get; init; }
}
