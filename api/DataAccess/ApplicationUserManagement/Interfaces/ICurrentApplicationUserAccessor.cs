namespace PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;

public interface ICurrentApplicationUserAccessor
{
    int GetApplicationUserId();
    int? GetApplicationUserIdOrDefault();
    string GetApplicationUserRole();
}
