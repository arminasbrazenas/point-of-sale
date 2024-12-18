using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceChargeValidationService
{
    Task<string> ValidateName(string name, int businessId);
    decimal ValidateAmount(decimal amount, PricingStrategy pricingStrategy);
}
