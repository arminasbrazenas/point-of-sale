using PointOfSale.BusinessLogic.ServiceManagement.DTOs;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IServiceResourceService
{
    public Task<ServiceResourceDTO> CreateServiceResource(CreateServiceResourceDTO createServiceResourceDto);
    public Task<ServiceResourceDTO> UpdateServiceResource(int serviceResourceId, UpdateServiceResourceDTO updateServiceResourceDto);
    public Task DeleteServiceResource(int serviceResourceId);
    public Task<ServiceResourceDTO> GetServiceResource(int serviceResourceId);
}