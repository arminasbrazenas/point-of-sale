using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.Models.ApplicationUserManagement.Enums;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserValidationService : IApplicationUserValidationService
{
    private readonly IBusinessService _businessService;

    public ApplicationUserValidationService(IBusinessService businessService)
    {
        _businessService = businessService;
    }
    public void ValidateApplicationUserRole(string role)
    {
        if (!Enum.IsDefined(typeof(Roles), role) && !Enum.TryParse<Roles>(role, ignoreCase: true, out _))
        {
            throw new ValidationException(new InvalidRoleErrorMessage(role));
        }
    }

    async public Task ValidateRegisterApplicationUserDTO(RegisterApplicationUserDTO dto)
    {
        if (dto.BusinessId is int businessId && await _businessService.GetBusiness(businessId) is null){
            throw new ValidationException(new InvalidBusinessIdErrorMessage(businessId));
        }
    }
}
