using PointOfSale.BusinessLogic.ServiceManagement.DTOs;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IServiceService
{
    public Task<ServiceDTO> CreateService(CreateServiceDTO createServiceDto);
    public Task<ServiceDTO> UpdateService(int serviceId, UpdateServiceDTO updateServiceDto);
    public Task DeleteService(int serviceId);
    public Task<ServiceDTO> GetService(int serviceId);
}