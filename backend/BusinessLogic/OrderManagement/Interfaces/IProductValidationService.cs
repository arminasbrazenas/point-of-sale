using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IProductValidationService
{
    Task<string> ValidateName(string name);
    decimal ValidatePrice(decimal price);
    int ValidateStock(int stock);
    Task<List<Tax>> ValidateTaxes(List<int> taxIds);
}
