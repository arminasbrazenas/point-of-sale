using PointOfSale.BusinessLogic.ServiceManagement.DTOs;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IServiceAvailabilityService
{
    public Task<ServiceAvailabilityDTO> CreateServiceAvailability(CreateServiceAvailabilityDTO createServiceAvailabilityDto);
    public Task<ServiceAvailabilityDTO> UpdateServiceAvailability(int serviceAvailabilityId, UpdateServiceAvailabilityDTO updateServiceAvailabilityDto);
    public Task DeleteServiceAvailability(int serviceAvailabilityId);
    public Task<ServiceAvailabilityDTO> GetServiceAvailability(int serviceAvailabilityId);
}