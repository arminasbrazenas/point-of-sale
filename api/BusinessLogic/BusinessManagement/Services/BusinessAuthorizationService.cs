using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessAuthorizationService : IBusinessAuthorizationService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public BusinessAuthorizationService(
        IBusinessRepository businessRepository,
        ICurrentApplicationUserAccessor currentApplicationUserAccessor,
        UserManager<ApplicationUser> userManager
    )
    {
        _businessRepository = businessRepository;
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
        _userManager = userManager;
    }

    public async Task AuthorizeBusinessViewAction(int businessId)
    {
        var currentUserRole = _currentApplicationUserAccessor.GetApplicationUserRole();
        var currentUserId = _currentApplicationUserAccessor.GetApplicationUserId();

        if (currentUserRole == "Admin")
        {
            return;
        }
        else if ((await _userManager.FindByIdAsync(currentUserId.ToString()))!.Business!.Id == businessId)
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
