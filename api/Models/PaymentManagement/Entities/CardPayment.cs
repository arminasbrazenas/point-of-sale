namespace PointOfSale.Models.PaymentManagement.Entities;

public class CardPayment : Payment
{
    public required string ExternalId { get; set; }
}
