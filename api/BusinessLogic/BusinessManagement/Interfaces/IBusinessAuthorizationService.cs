namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessAuthorizationService
{
    Task AuthorizeBusinessWriteAction(int? businessId = null);
    Task AuthorizeBusinessViewAction(int businessId);
}
