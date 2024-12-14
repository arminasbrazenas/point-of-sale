using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IServiceResourcesMappingService
{
    public ServiceResourceDTO MapToServiceResourceDTO(ServiceResource serviceResource);
}