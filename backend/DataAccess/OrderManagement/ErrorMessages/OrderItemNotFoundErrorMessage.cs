using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class OrderItemNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _orderItemId;

    public OrderItemNotFoundErrorMessage(int orderItemId)
    {
        _orderItemId = orderItemId;
    }

    public string En => $"Order item with id '{_orderItemId}' not found.";
}