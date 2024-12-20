namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface ITaxValidationService
{
    Task<string> ValidateName(string name, int businessId);
    decimal ValidateRate(decimal rate);
}
