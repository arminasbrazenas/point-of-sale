using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IDiscountValidationService
{
    decimal ValidateAmount(decimal amount, PricingStrategy pricingStrategy);
}
