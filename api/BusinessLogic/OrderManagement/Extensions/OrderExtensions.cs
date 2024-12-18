using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Interfaces;
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
            PricingStrategy.FixedAmount => discount.Amount.ToRoundedPrice(),
            PricingStrategy.Percentage => (price * (discount.Amount / 100m)).ToRoundedPrice(),
            _ => throw new ArgumentOutOfRangeException(
                nameof(discount.PricingStrategy),
                discount.PricingStrategy,
                null
            ),
        };

    public static decimal GetAmountToApply(this CreateOrderDiscountDTO discount, decimal price) =>
        discount.PricingStrategy switch
        {
            PricingStrategy.FixedAmount => discount.Amount.ToRoundedPrice(),
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

    public static decimal GetAmountToApply(this IServiceCharge serviceCharge, decimal price) =>
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

    public static decimal GetAmountToApply(this Tax tax, decimal price)
    {
        return (price * tax.Rate).ToRoundedPrice();
    }

    public static decimal RoundIfFixed(this decimal amount, PricingStrategy pricingStrategy) =>
        pricingStrategy switch
        {
            PricingStrategy.Percentage => amount,
            PricingStrategy.FixedAmount => amount.ToRoundedPrice(),
            _ => throw new NotImplementedException(),
        };
}
