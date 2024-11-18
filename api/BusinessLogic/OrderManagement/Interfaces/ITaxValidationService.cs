namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface ITaxValidationService
{
    Task<string> ValidateName(string name);
    decimal ValidateRate(decimal rate);
}
