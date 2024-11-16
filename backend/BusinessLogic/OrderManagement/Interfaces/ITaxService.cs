using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface ITaxService
{
    Task<TaxDTO> CreateTax(CreateTaxDTO createTaxDTO);
    Task<TaxDTO> GetTax(int taxId);
    Task<PagedResponseDTO<TaxDTO>> GetTaxes(PaginationFilterDTO paginationFilterDTO);
    Task DeleteTax(int taxId);
}
