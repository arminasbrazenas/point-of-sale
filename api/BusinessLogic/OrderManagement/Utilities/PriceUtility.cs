using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Utilities;

public static class PriceUtility
{
    public static decimal CalculateTotalTax(decimal basePrice, IEnumerable<decimal> taxRates)
    {
        return taxRates.Sum(taxRate => basePrice * taxRate);
    }

    public static decimal ToRoundedPrice(this decimal price)
    {
        return Math.Round(price, 2, MidpointRounding.AwayFromZero);
    }
    public static decimal CalculateTotalDue(IEnumerable<OrderServiceCharge> items)
    {
        return items.Sum(item => item.Amount);
    }
}
