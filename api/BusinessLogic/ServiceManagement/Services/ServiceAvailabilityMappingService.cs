using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceAvailabilityMappingService : IServiceAvailabilityMappingService
{
    public ServiceAvailabilityDTO MapToServiceAvailabilityDTO(ServiceAvailability serviceAvailability)
    {
        return new ServiceAvailabilityDTO
        {
            Id = serviceAvailability.Id,
            ServiceId = serviceAvailability.ServiceId,
            ServiceResourceId = serviceAvailability.ServiceResourceId,
            Priority = serviceAvailability.Priority,
        };
    }
}