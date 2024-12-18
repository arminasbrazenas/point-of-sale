using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
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
    private readonly IContactInfoValidationService _contactInfoValidationService;

    public BusinessValidationService(
        UserManager<ApplicationUser> userManager,
        IApplicationUserRepository applicationUserRepository,
        IContactInfoValidationService contactInfoValidationService
    )
    {
        _userManager = userManager;
        _applicationUserRepository = applicationUserRepository;
        _contactInfoValidationService = contactInfoValidationService;
    }

    public async Task ValidateCreateBusinessDTO(CreateBusinessDTO dto)
    {
        _contactInfoValidationService.ValidatePhoneNumber(dto.PhoneNumber);
        _contactInfoValidationService.ValidateEmail(dto.Email);
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

        ValidateTime(dto.StartHour, dto.StartMinute, dto.EndHour, dto.EndMinute);
    }

    public void ValidateTime(int startHour, int startMinute, int endHour, int endMinute)
    {
        if (startMinute < 0 || startMinute > 59)
        {
            throw new ValidationException(new InvalidWorkingHoursErrorMessage());
        }

        if (endMinute < 0 || endMinute > 59)
        {
            throw new ValidationException(new InvalidWorkingHoursErrorMessage());
        }

        if (startHour < 0 || startHour > 24)
        {
            throw new ValidationException(new InvalidWorkingHoursErrorMessage());
        }

        if (endHour < 0 || endHour > 23)
        {
            throw new ValidationException(new InvalidWorkingHoursErrorMessage());
        }

        if (startHour * 60 + startMinute > endHour * 60 + endMinute)
        {
            throw new ValidationException(new InvalidWorkingHoursErrorMessage());
        }
    }
}
