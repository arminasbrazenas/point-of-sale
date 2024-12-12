using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class OrderManagementAuthorizationService : IOrderManagementAuthorizationService
{
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderManagementAuthorizationService(
        ICurrentApplicationUserAccessor currentApplicationUserAccessor,
        UserManager<ApplicationUser> userManager
    )
    {
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
        _userManager = userManager;
    }

    public async Task AuthorizeApplicationUser(int businessId)
    {
        var currentUserId = _currentApplicationUserAccessor.GetApplicationUserId();

        var user = await _userManager.FindByIdAsync(currentUserId.ToString());

        if (user is null)
        {
            throw new ApplicationUserAuthenticationException(new ApplicationUserNotFoundErrorMessage(currentUserId));
        }
        else if (user.Business!.Id != businessId)
        {
            throw new ApplicationUserAuthorizationException(new ApplicationUserUnauthorizedErrorMessage());
        }
    }
}
