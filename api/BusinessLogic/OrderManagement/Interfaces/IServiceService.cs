using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceService
{
    Task<ServiceDTO> CreateService(CreateServiceDTO createServiceDTO);
    Task<ServiceDTO> UpdateService(int serviceId, UpdateServiceDTO updateServiceDto);
    Task DeleteService(int serviceId);
    Task<ServiceDTO> GetService(int serviceId);
    Task<PagedResponseDTO<ServiceDTO>> GetServices(PaginationFilterDTO paginationFilterDTO, int businessId);
}
