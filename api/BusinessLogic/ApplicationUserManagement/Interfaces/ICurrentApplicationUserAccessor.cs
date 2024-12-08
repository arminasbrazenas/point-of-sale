namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface ICurrentApplicationUserAccessor
{
    int GetApplicationUserId();
    string GetApplicationUserRole();
}
