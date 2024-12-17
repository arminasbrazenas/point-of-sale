using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ServiceMappingService : IServiceMappingService
{
    public ServiceDTO MapToServiceDTO(Service service)
    {
        return new ServiceDTO
        {
            Id = service.Id,
            Name = service.Name,
            Duration = service.Duration,
            Price = service.Price,
        };
    }
    
    public PagedResponseDTO<ServiceDTO> MapToPagedServiceDTO(List<Service> services, PaginationFilter paginationFilter, int totalCount)
    {
        return new PagedResponseDTO<ServiceDTO>
        {
            Items = services.Select(MapToServiceDTO).ToList(),
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Page = paginationFilter.Page,
        };
    }
}