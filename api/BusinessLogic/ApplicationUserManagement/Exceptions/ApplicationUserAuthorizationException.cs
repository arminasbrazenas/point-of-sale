using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;

public class ApplicationUserAuthorizationException : PointOfSaleException
{
    public ApplicationUserAuthorizationException(IPointOfSaleErrorMessage errorMessage)
        : base(errorMessage) { }
}
