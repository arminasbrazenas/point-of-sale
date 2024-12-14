using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceMappingService : IServiceMappingService
{
    public ServiceDTO MapToServiceDTO(Service service)
    {
        return new ServiceDTO
        {
            Id = service.Id,
            Name = service.Name,
            AvailableFrom = service.AvailableFrom,
            AvailableTo = service.AvailableTo,
            Duration = service.Duration,
            Price = service.Price,
        };
    }
}