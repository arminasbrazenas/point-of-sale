using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.PaymentManagement.ErrorMessages;

public class GiftCardNotFoundErrorMessage(int giftCardId) : IPointOfSaleErrorMessage
{
    public string En => $"Gift card by id {giftCardId} not found.";
}

public class DuplicateGiftCardCodeErrorMessage(string code) : IPointOfSaleErrorMessage
{
    public string En => $"Gift card code '{code}' is already used.";
}

public class UsedGiftCardCannotBeModifiedErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Used gift card cannot be modified.";
}
