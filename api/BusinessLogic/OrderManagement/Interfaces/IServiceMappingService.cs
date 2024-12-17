using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceMappingService
{
    ServiceDTO MapToServiceDTO(Service service);
    ServiceEmployeeDTO MapToServiceEmployeeDTO(ApplicationUser applicationUser);
    PagedResponseDTO<ServiceDTO> MapToPagedServiceDTO(
        List<Service> services,
        PaginationFilter paginationFilter,
        int totalCount
    );
}