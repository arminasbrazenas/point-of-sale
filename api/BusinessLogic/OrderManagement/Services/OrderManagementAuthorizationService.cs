using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class OrderManagementAuthorizationService : IOrderManagementAuthorizationService
{
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public OrderManagementAuthorizationService(
        ICurrentApplicationUserAccessor currentApplicationUserAccessor,
        IApplicationUserRepository applicationUserRepository
    )
    {
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task AuthorizeApplicationUser(int businessId)
    {
        var currentUserId = _currentApplicationUserAccessor.GetApplicationUserId();

        var user = await _applicationUserRepository.GetUserByIdWithBusinessAsync(currentUserId);

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
