namespace PointOfSale.BusinessLogic.OrderManagement.Utilities;

public static class PriceUtility
{
    public static decimal ToRoundedPrice(this decimal price)
    {
        return Math.Round(price, 2, MidpointRounding.AwayFromZero);
    }
}
