using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IServiceChargeMappingService
{
    ServiceChargeDTO MapToServiceChargeDTO(ServiceCharge serviceCharge);
    PagedResponseDTO<ServiceChargeDTO> MapToPagedServiceChargeDTO(
        List<ServiceCharge> serviceCharges,
        PaginationFilter paginationFilter,
        int totalCount
    );
}
