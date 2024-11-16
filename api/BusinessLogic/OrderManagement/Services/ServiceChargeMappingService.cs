using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ServiceChargeMappingService : IServiceChargeMappingService
{
    public ServiceChargeDTO MapToServiceChargeDTO(ServiceCharge serviceCharge)
    {
        return new ServiceChargeDTO
        {
            Id = serviceCharge.Id,
            Name = serviceCharge.Name,
            Amount = serviceCharge.Amount,
            PricingStrategy = serviceCharge.PricingStrategy,
        };
    }

    public PagedResponseDTO<ServiceChargeDTO> MapToPagedServiceChargeDTO(
        List<ServiceCharge> serviceCharges,
        PaginationFilter paginationFilter
    )
    {
        return new PagedResponseDTO<ServiceChargeDTO>
        {
            Items = serviceCharges.Select(MapToServiceChargeDTO).ToList(),
            ItemsPerPage = paginationFilter.ItemsPerPage,
            Page = paginationFilter.Page,
        };
    }
}
