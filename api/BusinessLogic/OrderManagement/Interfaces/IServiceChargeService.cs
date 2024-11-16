using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceChargeService
{
    Task<ServiceChargeDTO> CreateServiceCharge(CreateServiceChargeDTO serviceChargeDTO);
    Task<ServiceChargeDTO> GetServiceCharge(int serviceChargeId);
    Task<PagedResponseDTO<ServiceChargeDTO>> GetServiceCharges(PaginationFilterDTO paginationFilterDTO);
    Task DeleteServiceCharge(int serviceChargeId);
}
