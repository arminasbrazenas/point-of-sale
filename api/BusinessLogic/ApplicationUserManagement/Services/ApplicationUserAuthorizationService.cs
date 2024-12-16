using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserAuthorizationService : IApplicationUserAuthorizationService
{
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserAuthorizationService(
        ICurrentApplicationUserAccessor currentApplicationUserAccessor,
        IApplicationUserRepository applicationUserRepository,
        UserManager<ApplicationUser> userManager
    )
    {
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
        _applicationUserRepository = applicationUserRepository;
        _userManager = userManager;
    }

    public async Task AuthorizeApplicationUserAction(int applicationUserId)
    {
        var currentUserId = _currentApplicationUserAccessor.GetApplicationUserId();
        var currentUser = await _applicationUserRepository.GetUserByIdWithBusinessAsync(currentUserId);
        var currentUserRole = _currentApplicationUserAccessor.GetApplicationUserRole();

        if (currentUser == null)
        {
            throw new ApplicationUserAuthenticationException(new ApplicationUserNotFoundErrorMessage(currentUserId));
        }

        if (currentUserRole == "Admin")
        {
            return;
        }
        else if (currentUserId == applicationUserId)
        {
            return;
        }
        else if (currentUserRole == "BusinessOwner")
        {
            if (currentUser.OwnedBusiness is null)
            {
                Console.WriteLine("current user business is null");
                throw new ApplicationUserAuthorizationException(new ApplicationUserUnauthorizedErrorMessage());
            }
            else
            {
                var applicationUser = await _applicationUserRepository.GetUserByIdWithBusinessAsync(currentUserId);
                if (applicationUser is null)
                {
                    Console.WriteLine("application user is null");
                    throw new ApplicationUserAuthenticationException(
                        new ApplicationUserNotFoundErrorMessage(applicationUserId)
                    );
                }
                if (applicationUser.EmployerBusiness != currentUser.OwnedBusiness)
                {
                    Console.WriteLine("businesses do not match");
                    throw new ApplicationUserAuthorizationException(new ApplicationUserUnauthorizedErrorMessage());
                }
            }
        }
    }

    public async Task AuthorizeGetApplicationUsersAction(int? businessId)
    {
        var currentUserId = _currentApplicationUserAccessor.GetApplicationUserId();
        var currentUser = await _applicationUserRepository.GetUserByIdWithBusinessAsync(currentUserId);
        var currentUserRole = _currentApplicationUserAccessor.GetApplicationUserRole();

        if (businessId is null && currentUserRole != "Admin")
        {
            throw new ApplicationUserAuthorizationException(new ApplicationUserUnauthorizedErrorMessage());
        }
    }
}
