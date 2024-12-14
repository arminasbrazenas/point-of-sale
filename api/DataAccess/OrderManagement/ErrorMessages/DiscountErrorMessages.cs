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

public class EntitledDiscountMustHaveProductsErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Entitled discount must have products.";
}

public class EverythingDiscountCannotBeAppliedToProductsErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Everything discount cannot be applied to products.";
}
