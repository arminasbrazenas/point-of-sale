using System.Text.RegularExpressions;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Enums;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserValidationService : IApplicationUserValidationService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IContactInfoValidationService _contactInfoValidationService;

    public ApplicationUserValidationService(
        IBusinessRepository businessRepository,
        IContactInfoValidationService contactInfoValidationService
    )
    {
        _businessRepository = businessRepository;
        _contactInfoValidationService = contactInfoValidationService;
    }

    public void ValidateApplicationUserRole(string role)
    {
        if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            throw new ValidationException(new InvalidRoleErrorMessage(role));
        }
        if (!Enum.IsDefined(typeof(Roles), role) && !Enum.TryParse<Roles>(role, ignoreCase: true, out _))
        {
            throw new ValidationException(new InvalidRoleErrorMessage(role));
        }
    }

    public async Task ValidateRegisterApplicationUserDTO(RegisterApplicationUserDTO dto)
    {
        _contactInfoValidationService.ValidateEmail(dto.Email);
        _contactInfoValidationService.ValidatePhoneNumber(dto.PhoneNumber);
        if (dto.BusinessId is not null && dto.Role == "BusinessOwner")
        {
            throw new ValidationException(
                new FailedActionOnApplicationUserErrorMessage("Cannot create BusinessOwner with pre-existing business.")
            );
        }

        if (dto.BusinessId is null && dto.Role == "Employee")
        {
            throw new ValidationException(
                new FailedActionOnApplicationUserErrorMessage("Cannot create Employee without pre-existing business.")
            );
        }

        if (dto.BusinessId is int businessId && await _businessRepository.Get(businessId) is null)
        {
            throw new ValidationException(new InvalidBusinessIdErrorMessage(businessId));
        }
    }
}
