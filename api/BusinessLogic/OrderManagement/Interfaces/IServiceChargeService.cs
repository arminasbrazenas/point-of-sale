using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceChargeService
{
    Task<ServiceChargeDTO> CreateServiceCharge(CreateServiceChargeDTO serviceChargeDTO);
    Task<ServiceChargeDTO> GetServiceCharge(int serviceChargeId);
    Task<PagedResponseDTO<ServiceChargeDTO>> GetServiceCharges(PaginationFilterDTO paginationFilterDTO, int businessId);
    Task<ServiceChargeDTO> UpdateServiceCharge(int serviceChargeId, UpdateServiceChargeDTO updateServiceChargeDTO);
    Task DeleteServiceCharge(int serviceChargeId);
}
