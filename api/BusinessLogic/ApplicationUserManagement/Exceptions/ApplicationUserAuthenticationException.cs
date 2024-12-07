using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;

public class ApplicationUserAuthenticationException : PointOfSaleException
{
    public ApplicationUserAuthenticationException(IPointOfSaleErrorMessage errorMessage)
        : base(errorMessage) { }
}
