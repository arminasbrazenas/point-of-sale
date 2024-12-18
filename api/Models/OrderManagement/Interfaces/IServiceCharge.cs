using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.Models.OrderManagement.Interfaces;

public interface IServiceCharge
{
    string Name { get; set; }
    PricingStrategy PricingStrategy { get; set; }
    decimal Amount { get; set; }
}
