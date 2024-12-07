using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserAuthorizationService
{
    string GetApplicationUserAccessToken(ApplicationUser user, string role);
    Task<string> GetApplicationUserRefreshToken(ApplicationUser user, string role);
}
