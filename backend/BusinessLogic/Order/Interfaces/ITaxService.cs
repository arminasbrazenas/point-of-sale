using PointOfSale.BusinessLogic.Order.DTOs;

namespace PointOfSale.BusinessLogic.Order.Interfaces;

public interface ITaxService
{
    Task<TaxDTO> CreateTax(CreateTaxDTO createTaxDTO);
    Task<TaxDTO> GetTax(int taxId);
    Task DeleteTax(int taxId);
}
