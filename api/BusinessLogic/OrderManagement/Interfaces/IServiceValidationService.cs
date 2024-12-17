namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceValidationService
{
    Task<string> ValidateName(string name);
    decimal ValidatePrice(decimal price);
}