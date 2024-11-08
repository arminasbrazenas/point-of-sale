using PointOfSale.BusinessLogic.OrderManagement.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface ITaxService
{
    Task<TaxDTO> CreateTax(CreateTaxDTO createTaxDTO);
    Task<TaxDTO> GetTax(int taxId);
    Task DeleteTax(int taxId);
}
