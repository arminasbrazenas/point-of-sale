using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IServiceAvailabilityMappingService
{
    public ServiceAvailabilityDTO MapToServiceAvailabilityDTO(ServiceAvailability serviceAvailability);
}