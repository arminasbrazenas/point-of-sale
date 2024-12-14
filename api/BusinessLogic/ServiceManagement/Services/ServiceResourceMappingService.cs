using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceResourceMappingService : IServiceResourcesMappingService
{
    public ServiceResourceDTO MapToServiceResourceDTO(ServiceResource serviceResource)
    {
        return new ServiceResourceDTO
        {
            Id = serviceResource.Id,
            Name = serviceResource.Name,
        };
    }
}