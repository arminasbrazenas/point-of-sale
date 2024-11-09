using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class CannotModifyNonOpenOrderErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Non-open order cannot be modified.";
}
