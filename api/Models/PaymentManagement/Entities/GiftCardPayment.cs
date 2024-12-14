namespace PointOfSale.Models.PaymentManagement.Entities;

public class GiftCardPayment : Payment
{
    public required string GiftCardCode { get; set; }
}
