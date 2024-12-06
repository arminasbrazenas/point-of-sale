using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.Shared.Migrations;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessValidationService : IBusinessValidationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public BusinessValidationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    async public Task ValidateCreateBusinessDTO(CreateBusinessDTO dto){
        var owner = await _userManager.FindByIdAsync(dto.BusinessOwnerId.ToString());

        if (owner is null){
            throw new ValidationException(new InvalidBusinessOwnerIdErrorMessage(dto.BusinessOwnerId));
        }
    }
}