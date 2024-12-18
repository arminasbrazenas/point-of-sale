namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IModifierValidationService
{
    Task<string> ValidateName(string name, int businessId);
    int ValidateStock(int stock);
}
