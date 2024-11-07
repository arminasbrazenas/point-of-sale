using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Exceptions;

public class ValidationException : PointOfSaleException
{
    public ValidationException(IPointOfSaleErrorMessage errorMessage) : base(errorMessage)
    {
    }
}