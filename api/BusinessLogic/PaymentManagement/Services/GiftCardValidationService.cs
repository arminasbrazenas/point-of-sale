using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.PaymentManagement;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;

namespace PointOfSale.BusinessLogic.PaymentManagement.Services;

public class GiftCardValidationService : IGiftCardValidationService
{
    private readonly IGiftCardRepository _giftCardRepository;

    public GiftCardValidationService(IGiftCardRepository giftCardRepository)
    {
        _giftCardRepository = giftCardRepository;
    }

    public async Task<string> ValidateCode(string code, int businessId)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ValidationException(new GiftCardCodeMustNotBeEmptyErrorMessage());
        }

        if (code.Length > Constants.GiftCardCodeMaxLength)
        {
            throw new ValidationException(new GiftCardCodeTooLongErrorMessage());
        }

        if (await _giftCardRepository.IsCodeUsed(code, businessId))
        {
            throw new ValidationException(new DuplicateGiftCardCodeErrorMessage(code));
        }

        return code;
    }

    public decimal ValidateAmount(decimal amount)
    {
        if (amount <= 0m)
        {
            throw new ValidationException(new GiftCardAmountMustBePositiveErrorMessage());
        }

        return amount;
    }

    public DateTimeOffset ValidateExpiration(DateTimeOffset expiresAt)
    {
        if (expiresAt <= DateTimeOffset.UtcNow)
        {
            throw new ValidationException(new GiftCardExpirationDateMustBeFutureDateErrorMessage());
        }

        return expiresAt;
    }
}
