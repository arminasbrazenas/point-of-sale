using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class TaxMappingService : ITaxMappingService
{
    public TaxDTO MapToTaxDTO(Tax tax)
    {
        return new TaxDTO
        {
            Id = tax.Id,
            Name = tax.Name,
            Rate = tax.Rate,
        };
    }

    public PagedResponseDTO<TaxDTO> MapToPagedTaxDTO(List<Tax> taxes, PaginationFilter paginationFilter, int totalCount)
    {
        return new PagedResponseDTO<TaxDTO>
        {
            Items = taxes.Select(MapToTaxDTO).ToList(),
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Page = paginationFilter.Page,
        };
    }
}
