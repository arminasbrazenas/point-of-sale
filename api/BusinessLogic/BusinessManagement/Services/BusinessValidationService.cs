using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessValidationService : IBusinessValidationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public BusinessValidationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ValidateCreateBusinessDTO(CreateBusinessDTO dto)
    {
        var owner = await _userManager.FindByIdAsync(dto.BusinessOwnerId.ToString());

        if (owner is null)
        {
            throw new ValidationException(new InvalidBusinessOwnerIdErrorMessage(dto.BusinessOwnerId));
        }

        var role = (await _userManager.GetRolesAsync(owner)).FirstOrDefault();

        if (role != "BusinessOwner")
        {
            throw new ValidationException(new InvalidApplicationUserRoleToOwnBusinessErrorMessage());
        }
    }

    public async Task ValidateUpdateBusinessDTO(UpdateBusinessDTO dto)
    {
        if (dto.BusinessOwnerId is not null)
        {
            ApplicationUser? newBusinessOwner = await _userManager.FindByIdAsync(dto.BusinessOwnerId.Value.ToString());
            if (newBusinessOwner is null)
            {
                throw new ValidationException(new InvalidBusinessOwnerIdErrorMessage(dto.BusinessOwnerId.Value));
            }
            else if ((await _userManager.GetRolesAsync(newBusinessOwner)).FirstOrDefault() != "BusinessOwner")
            {
                throw new ValidationException(new InvalidApplicationUserRoleToOwnBusinessErrorMessage());
            }
            else if (newBusinessOwner.Business is not null)
            {
                throw new ValidationException(
                    new ApplicationUserCannotOwnMultipleBusinessesErrorMessage(newBusinessOwner.Id)
                );
            }
        }
    }
}
