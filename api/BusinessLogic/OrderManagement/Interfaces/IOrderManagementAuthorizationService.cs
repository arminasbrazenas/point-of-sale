namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderManagementAuthorizationService
{
    Task AuthorizeApplicationUser(int businessId);
}
