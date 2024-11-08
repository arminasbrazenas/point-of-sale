using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.Shared.Exceptions;

public class ValidationException : PointOfSaleException
{
    public ValidationException(IPointOfSaleErrorMessage errorMessage)
        : base(errorMessage) { }
}
