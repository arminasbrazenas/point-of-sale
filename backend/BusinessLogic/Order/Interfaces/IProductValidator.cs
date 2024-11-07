using PointOfSale.DataAccess.Order.Entities;

namespace PointOfSale.BusinessLogic.Order.Interfaces;

public interface IProductValidator
{
    Task<string> ValidateName(string name);
    decimal ValidatePrice(decimal price);
    int ValidateStock(int stock);
    Task<List<Tax>> ValidateTaxes(List<int> taxIds);
}