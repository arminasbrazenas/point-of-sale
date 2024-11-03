using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Order.ErrorMessages;

public class ProductNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _productId;

    public ProductNotFoundErrorMessage(int productId)
    {
        _productId = productId;
    }

    public string En => $"Product with id '{_productId}' not found.";
}
