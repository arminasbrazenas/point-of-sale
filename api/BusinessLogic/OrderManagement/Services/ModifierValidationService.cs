using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.OrderManagement;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ModifierValidationService : IModifierValidationService
{
    private readonly IModifierRepository _modifierRepository;

    public ModifierValidationService(IModifierRepository modifierRepository)
    {
        _modifierRepository = modifierRepository;
    }

    public async Task<string> ValidateName(string name, int businessId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new ModifierNameMustNotBeEmptyErrorMessage());
        }

        if (name.Length > Constants.ModifierNameMaxLength)
        {
            throw new ValidationException(new ModifierNameTooLongErrorMessage());
        }

        if (await _modifierRepository.ExistsWithName(name, businessId))
        {
            throw new ValidationException(new ModifierNameConflictErrorMessage());
        }

        return name;
    }

    public int ValidateStock(int stock)
    {
        if (stock < 0)
        {
            throw new ValidationException(new ModifierQuantityMustNotBeNegativeErrorMessage());
        }

        return stock;
    }
}
