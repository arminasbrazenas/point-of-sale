using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class OrderNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _orderId;

    public OrderNotFoundErrorMessage(int orderId)
    {
        _orderId = orderId;
    }

    public string En => $"Order with id '{_orderId}' not found.";
}

public class CannotModifyNonOpenOrderErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Non-open order cannot be modified.";
}
