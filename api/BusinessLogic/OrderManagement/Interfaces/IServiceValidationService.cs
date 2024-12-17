namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceValidationService
{
    Task<string> ValidateName(string name, int businessId);
    decimal ValidatePrice(decimal price);
    int ValidateDurationInMinutes(int durationInMinutes);
}
