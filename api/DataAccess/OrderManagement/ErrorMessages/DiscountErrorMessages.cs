using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class DiscountNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _discountId;

    public DiscountNotFoundErrorMessage(int discountId)
    {
        _discountId = discountId;
    }

    public string En => $"Discount with id {_discountId} was not found.";
}
