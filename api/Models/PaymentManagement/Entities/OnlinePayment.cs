namespace PointOfSale.Models.PaymentManagement.Entities;

public class OnlinePayment : Payment
{
    public required string ExternalId { get; set; }
}
