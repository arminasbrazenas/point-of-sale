using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface ITaxService
{
    Task<TaxDTO> CreateTax(CreateTaxDTO createTaxDTO);
    Task<TaxDTO> UpdateTax(int taxId, UpdateTaxDTO updateTaxDTO);
    Task<TaxDTO> GetTax(int taxId);
    Task<PagedResponseDTO<TaxDTO>> GetTaxes(int businessId, PaginationFilterDTO paginationFilterDTO);
    Task DeleteTax(int taxId);
}
