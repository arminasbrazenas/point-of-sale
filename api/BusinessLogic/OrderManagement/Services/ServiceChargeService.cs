using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ServiceChargeService : IServiceChargeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceChargeRepository _serviceChargeRepository;
    private readonly IServiceChargeMappingService _serviceChargeMappingService;

    public ServiceChargeService(
        IUnitOfWork unitOfWork,
        IServiceChargeRepository serviceChargeRepository,
        IServiceChargeMappingService serviceChargeMappingService
    )
    {
        _unitOfWork = unitOfWork;
        _serviceChargeRepository = serviceChargeRepository;
        _serviceChargeMappingService = serviceChargeMappingService;
    }

    public async Task<ServiceChargeDTO> CreateServiceCharge(CreateServiceChargeDTO serviceChargeDTO)
    {
        var serviceCharge = new ServiceCharge
        {
            Name = serviceChargeDTO.Name,
            PricingStrategy = serviceChargeDTO.PricingStrategy,
            Amount = serviceChargeDTO.Amount,
        };

        _serviceChargeRepository.Add(serviceCharge);
        await _unitOfWork.SaveChanges();

        return _serviceChargeMappingService.MapToServiceChargeDTO(serviceCharge);
    }

    public async Task<ServiceChargeDTO> GetServiceCharge(int serviceChargeId)
    {
        var serviceCharge = await _serviceChargeRepository.Get(serviceChargeId);
        return _serviceChargeMappingService.MapToServiceChargeDTO(serviceCharge);
    }

    public async Task<PagedResponseDTO<ServiceChargeDTO>> GetServiceCharges(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var serviceCharges = await _serviceChargeRepository.GetPagedWithTaxes(paginationFilter);
        var totalCount = await _serviceChargeRepository.GetTotalCount();
        return _serviceChargeMappingService.MapToPagedServiceChargeDTO(serviceCharges, paginationFilter, totalCount);
    }

    public async Task<ServiceChargeDTO> UpdateServiceCharge(
        int serviceChargeId,
        UpdateServiceChargeDTO updateServiceChargeDTO
    )
    {
        var serviceCharge = await _serviceChargeRepository.Get(serviceChargeId);

        if (updateServiceChargeDTO.Name is not null)
        {
            serviceCharge.Name = updateServiceChargeDTO.Name;
        }

        if (updateServiceChargeDTO.Amount.HasValue)
        {
            serviceCharge.Amount = updateServiceChargeDTO.Amount.Value;
        }

        if (updateServiceChargeDTO.PricingStrategy.HasValue)
        {
            serviceCharge.PricingStrategy = updateServiceChargeDTO.PricingStrategy.Value;
        }

        await _unitOfWork.SaveChanges();
        return _serviceChargeMappingService.MapToServiceChargeDTO(serviceCharge);
    }

    public async Task DeleteServiceCharge(int serviceChargeId)
    {
        await _serviceChargeRepository.Delete(serviceChargeId);
    }
}
