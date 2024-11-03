using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Exceptions;

public class EntityNotFoundException : PointOfSaleException
{
    public EntityNotFoundException(IPointOfSaleErrorMessage errorMessage)
        : base(errorMessage) { }
}
