using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.OrderManagement;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class TaxValidationService : ITaxValidationService
{
    private readonly ITaxRepository _taxRepository;

    public TaxValidationService(ITaxRepository taxRepository)
    {
        _taxRepository = taxRepository;
    }

    public async Task<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new TaxNameEmptyErrorMessage());
        }

        name = name.Trim();
        if (name.Length > Constants.TaxNameMaxLength)
        {
            throw new ValidationException(new TaxNameTooLongErrorMessage(Constants.TaxNameMaxLength));
        }

        var existingTax = await _taxRepository.GetByNameOptional(name);
        if (existingTax is not null)
        {
            throw new ValidationException(new TaxNameConflictErrorMessage(name));
        }

        return name;
    }

    public decimal ValidateRate(decimal rate)
    {
        if (rate is < 0m or > 1m)
        {
            throw new ValidationException(new TaxRateInvalidErrorMessage());
        }

        return rate;
    }
}
