using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class DiscountMappingService : IDiscountMappingService
{
    public DiscountDTO MapToDiscountDTO(Discount discount)
    {
        return new DiscountDTO
        {
            Amount = discount.Amount,
            PricingStrategy = discount.PricingStrategy,
            AppliesToProductIds = discount.AppliesTo.Select(p => p.Id).ToList(),
            ValidUntil = discount.ValidUntil
        };
    }
}