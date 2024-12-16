using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessMappingService : IBusinessMappingService
{
    public BusinessDTO MapToBusinessDTO(Business business)
    {
        return new BusinessDTO
        {
            Id = business.Id,
            BusinessOwnerId = business.BusinessOwnerId,
            Name = business.Name,
            Address = business.Address,
            Email = business.Email,
            PhoneNumber = business.TelephoneNumber,
        };
    }

    public PagedResponseDTO<BusinessDTO> MapToPagedBusinessDTO(
        List<Business> businesses,
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<BusinessDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Items = businesses.Select(MapToBusinessDTO).ToList(),
        };
    }
}
