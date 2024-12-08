using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class CurrentApplicationUserAccessor : ICurrentApplicationUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentApplicationUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetApplicationUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new ApplicationUserAuthorizationException(new NoApplicationUserIdErrorMessage());
        }

        return int.Parse(userIdClaim);
    }

    public string GetApplicationUserRole()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value
            ?? throw new ApplicationUserAuthorizationException(new NoApplicationUserRoleErrorMessage());
    }
}
