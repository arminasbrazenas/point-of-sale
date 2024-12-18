using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessAuthorizationService : IBusinessAuthorizationService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public BusinessAuthorizationService(
        IBusinessRepository businessRepository,
        ICurrentApplicationUserAccessor currentApplicationUserAccessor,
        IApplicationUserRepository applicationUserRepository
    )
    {
        _businessRepository = businessRepository;
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task AuthorizeBusinessViewAction(int businessId)
    {
        var currentUserRole = _currentApplicationUserAccessor.GetApplicationUserRole();
        var currentUserId = _currentApplicationUserAccessor.GetApplicationUserId();

        if (currentUserRole == "Admin")
        {
            return;
        }

        var user = await _applicationUserRepository.GetUserByIdWithBusinessAsync(currentUserId);

        if (user is null || !user.IsActive)
        {
            throw new ApplicationUserAuthorizationException(new UnauthorizedAccessToBusinessErrorMessage(businessId));
        }

        if (user?.OwnedBusiness == null)
        {
            throw new ApplicationUserAuthorizationException(new UnauthorizedAccessToBusinessErrorMessage(businessId));
        }
        else if (user.OwnedBusiness.Id == businessId)
        {
            return;
        }
        throw new ApplicationUserAuthorizationException(new UnauthorizedAccessToBusinessErrorMessage(businessId));
    }

    public async Task AuthorizeBusinessWriteAction(int? businessId)
    {
        var currentUserRole = _currentApplicationUserAccessor.GetApplicationUserRole();
        var currentUserId = _currentApplicationUserAccessor.GetApplicationUserId();

        if (currentUserRole == "Admin")
        {
            return;
        }

        if (currentUserRole == "BusinessOwner")
        {
            if (
                businessId is null
                || (await _businessRepository.Get(businessId.Value)).BusinessOwnerId == currentUserId
            )
            {
                return;
            }
        }

        throw new ApplicationUserAuthorizationException(
            new UnauthorizedAccessToBusinessErrorMessage(businessId!.Value)
        );
    }
}
