using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessMappingService
{
    BusinessDTO MapToBusinessDTO(Business business);
    PagedResponseDTO<BusinessDTO> MapToPagedBusinessDTO(
        List<Business> businesses,
        PaginationFilter paginationFilter,
        int totalCount
    );
}
