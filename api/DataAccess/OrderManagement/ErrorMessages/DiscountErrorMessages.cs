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

public class OrderDiscountCannotBeAppliedToProductsErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Order discount cannot be applied to products.";
}

public class DiscountAmountMustBePositiveErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Discount amount must be positive.";
}

public class DiscountPercentageInvalidErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Discount percentage must be between 0 and 100.";
}
