using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Extensions;

public static class OrderExtensions
{
    public static decimal GetAmountToApply(this IEnumerable<Discount> discounts, decimal price)
    {
        return discounts
            .OrderBy(d => d.PricingStrategy)
            .Aggregate(0m, (acc, discount) => acc + discount.GetAmountToApply(price - acc));
    }

    public static decimal GetAmountToApply(this Discount discount, decimal price) =>
        discount.PricingStrategy switch
        {
            PricingStrategy.FixedAmount => (price - discount.Amount).ToRoundedPrice(),
            PricingStrategy.Percentage => (price * (discount.Amount / 100m)).ToRoundedPrice(),
            _ => throw new ArgumentOutOfRangeException(
                nameof(discount.PricingStrategy),
                discount.PricingStrategy,
                null
            ),
        };

    public static decimal GetAmountToApply(this IEnumerable<Tax> taxes, decimal price)
    {
        return taxes.Aggregate(0m, (acc, tax) => acc + tax.GetAmountToApply(price + acc));
    }

    public static decimal GetAmountToApply(this ServiceCharge serviceCharge, decimal price) =>
        serviceCharge.PricingStrategy switch
        {
            PricingStrategy.FixedAmount => serviceCharge.Amount.ToRoundedPrice(),
            PricingStrategy.Percentage => (price * serviceCharge.Amount / 100m).ToRoundedPrice(),
            _ => throw new ArgumentOutOfRangeException(
                nameof(serviceCharge.PricingStrategy),
                serviceCharge.PricingStrategy,
                null
            ),
        };

    public static decimal GetAmountToApply(this IEnumerable<ServiceCharge> serviceCharges, decimal price)
    {
        return serviceCharges
            .OrderBy(d => d.PricingStrategy)
            .Aggregate(0m, (acc, c) => acc + c.GetAmountToApply(price + acc));
    }

    public static decimal GetAmountToApply(this Tax tax, decimal price)
    {
        return (price * tax.Rate).ToRoundedPrice();
    }

    public static decimal GetOrderItemsPrice(this Order order)
    {
        var price = 0m;
        foreach (var orderItem in order.Items)
        {
            var discounts = orderItem.Discounts.Sum(d => d.AppliedUnitAmount);
            var modifiers = orderItem.Modifiers.Sum(m => m.GrossPrice + m.TaxTotal);
            var taxes = orderItem.Taxes.Sum(t => t.AppliedUnitAmount);
            var unitPrice = orderItem.BaseUnitGrossPrice + modifiers - discounts + taxes;
            price += unitPrice * orderItem.Quantity;
        }

        return price;
    }
}
