using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IProductValidationService
{
    Task<string> ValidateName(string name, int businessId);
    decimal ValidatePrice(decimal price);
    int ValidateStock(int stock);
    Task<List<Tax>> ValidateTaxes(List<int> taxIds);
    Task<List<Modifier>> ValidateModifiers(List<int> modifierIds);
}
