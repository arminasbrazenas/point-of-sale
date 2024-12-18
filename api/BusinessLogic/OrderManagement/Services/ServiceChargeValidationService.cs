using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.OrderManagement;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ServiceChargeValidationService : IServiceChargeValidationService
{
    private readonly IServiceChargeRepository _serviceChargeRepository;

    public async Task<string> ValidateName(string name, int businessId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new ServiceChargeNameEmptyErrorMessage());
        }

        if (name.Length > Constants.ServiceChargeNameMaxLength)
        {
            throw new ValidationException(new ServiceChargeNameTooLongErrorMessage());
        }

        var existingServiceCharge = await _serviceChargeRepository.GetByNameOptional(name, businessId);
        if (existingServiceCharge is not null)
        {
            throw new ValidationException(new ServiceChargeNameConflictErrorMessage());
        }

        return name;
    }

    public decimal ValidateAmount(decimal amount, PricingStrategy pricingStrategy) =>
        pricingStrategy switch
        {
            PricingStrategy.Percentage => ValidatePercentageAmount(amount),
            PricingStrategy.FixedAmount => ValidateFixedAmount(amount),
        };

    private static decimal ValidateFixedAmount(decimal amount)
    {
        if (amount < 0m)
        {
            throw new ValidationException(new ServiceChargeAmountNegativeErrorMessage());
        }

        return amount;
    }

    private static decimal ValidatePercentageAmount(decimal amount)
    {
        if (amount is < 0m or > 100m)
        {
            throw new ValidationException(new ServiceChargeInvalidPercentageErrorMessage());
        }

        return amount;
    }
}
