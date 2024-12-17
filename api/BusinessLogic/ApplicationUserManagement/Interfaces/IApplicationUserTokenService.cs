using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserTokenService
{
    string GetApplicationUserAccessToken(ApplicationUser user, string role);
    Task<string> GetApplicationUserRefreshToken(ApplicationUser user, string role);
}
