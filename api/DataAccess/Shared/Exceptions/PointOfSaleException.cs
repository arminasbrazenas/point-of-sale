using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Exceptions;

public class PointOfSaleException : Exception
{
    public PointOfSaleException(IPointOfSaleErrorMessage errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public IPointOfSaleErrorMessage ErrorMessage { get; }
}
