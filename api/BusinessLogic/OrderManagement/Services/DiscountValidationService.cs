using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class DiscountValidationService : IDiscountValidationService
{
    public decimal ValidateAmount(decimal amount, PricingStrategy pricingStrategy) =>
        pricingStrategy switch
        {
            PricingStrategy.Percentage => ValidatePercentageAmount(amount),
            PricingStrategy.FixedAmount => ValidateFixedAmount(amount),
            _ => throw new NotImplementedException(),
        };

    private static decimal ValidateFixedAmount(decimal amount)
    {
        if (amount <= 0m)
        {
            throw new ValidationException(new DiscountAmountMustBePositiveErrorMessage());
        }

        return amount;
    }

    private static decimal ValidatePercentageAmount(decimal amount)
    {
        if (amount is <= 0m or > 100m)
        {
            throw new ValidationException(new DiscountPercentageInvalidErrorMessage());
        }

        return amount;
    }
}
