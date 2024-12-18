using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.PaymentManagement.ErrorMessages;

public class GiftCardNotFoundErrorMessage(int giftCardId) : IPointOfSaleErrorMessage
{
    public string En => $"Gift card by id {giftCardId} not found.";
}

public class GiftCardWithCodeNotFoundErrorMessage(string code) : IPointOfSaleErrorMessage
{
    public string En => $"Gift card with code '{code}' does not exist.";
}

public class DuplicateGiftCardCodeErrorMessage(string code) : IPointOfSaleErrorMessage
{
    public string En => $"Gift card code '{code}' is already used.";
}

public class UsedGiftCardCannotBeModifiedErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Used gift card cannot be modified.";
}

public class GiftCardIsAlreadyUsedErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Gift card is already used.";
}

public class GiftCardIsExpiredErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Gift card is expired.";
}

public class GiftCardExpirationDateMustBeFutureDateErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Gift card expiration date must be in the future.";
}

public class GiftCardAmountMustBePositiveErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Gift card amount must be positive.";
}

public class GiftCardCodeMustNotBeEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Gift card code must not be empty.";
}

public class GiftCardCodeTooLongErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Gift card code is too long.";
}
