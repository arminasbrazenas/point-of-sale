namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface ICurrentUserAccessor
{
    int GetApplicationUserId();
    string GetApplicationUserRole();
}
