using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessValidationService : IBusinessValidationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public BusinessValidationService(
        UserManager<ApplicationUser> userManager,
        IApplicationUserRepository applicationUserRepository
    )
    {
        _userManager = userManager;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task ValidateCreateBusinessDTO(CreateBusinessDTO dto)
    {
        var owner = await _applicationUserRepository.GetUserByIdWithBusinessAsync(dto.BusinessOwnerId);

        if (owner is null)
        {
            throw new ValidationException(new InvalidBusinessOwnerIdErrorMessage(dto.BusinessOwnerId));
        }

        var role = (await _userManager.GetRolesAsync(owner)).FirstOrDefault();

        if (role != "BusinessOwner")
        {
            throw new ValidationException(new InvalidApplicationUserRoleToOwnBusinessErrorMessage());
        }

        if (owner.OwnedBusiness is not null)
        {
            throw new ValidationException(
                new ApplicationUserCannotOwnMultipleBusinessesErrorMessage(dto.BusinessOwnerId)
            );
        }
    }
}
