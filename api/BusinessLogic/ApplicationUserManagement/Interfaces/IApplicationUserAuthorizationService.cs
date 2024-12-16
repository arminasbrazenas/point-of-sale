namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserAuthorizationService
{
    Task AuthorizeApplicationUserAction(int applicationUserId);
    Task AuthorizeGetApplicationUsersAction(int? businessId);
}
