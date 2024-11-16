using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface ITaxMappingService
{
    TaxDTO MapToTaxDTO(Tax tax);
    PagedResponseDTO<TaxDTO> MapToPagedTaxDTO(List<Tax> taxes, PaginationFilter paginationFilter);
}
