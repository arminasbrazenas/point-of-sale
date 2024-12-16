using PointOfSale.BusinessLogic.OrderManagement.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceService
{
    Task<ServiceDTO> CreateService(CreateServiceDTO createServiceDTO);
}