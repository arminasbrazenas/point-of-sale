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
    private readonly IOrderManagementAuthorizationService _orderManagementAuthorizationService;

    public ServiceChargeService(
        IUnitOfWork unitOfWork,
        IServiceChargeRepository serviceChargeRepository,
        IServiceChargeMappingService serviceChargeMappingService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService
    )
    {
        _unitOfWork = unitOfWork;
        _serviceChargeRepository = serviceChargeRepository;
        _serviceChargeMappingService = serviceChargeMappingService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
    }

    public async Task<ServiceChargeDTO> CreateServiceCharge(CreateServiceChargeDTO serviceChargeDTO)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(serviceChargeDTO.BusinessId);

        var serviceCharge = new ServiceCharge
        {
            Name = serviceChargeDTO.Name,
            PricingStrategy = serviceChargeDTO.PricingStrategy,
            Amount = serviceChargeDTO.Amount,
            BusinessId = serviceChargeDTO.BusinessId,
        };

        _serviceChargeRepository.Add(serviceCharge);
        await _unitOfWork.SaveChanges();

        return _serviceChargeMappingService.MapToServiceChargeDTO(serviceCharge);
    }

    public async Task<ServiceChargeDTO> GetServiceCharge(int serviceChargeId)
    {
        var serviceCharge = await _serviceChargeRepository.Get(serviceChargeId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(serviceCharge.BusinessId);

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

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(serviceCharge.BusinessId);

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
        var serviceCharge = await _serviceChargeRepository.Get(serviceChargeId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(serviceCharge.BusinessId);

        await _serviceChargeRepository.Delete(serviceChargeId);
    }
}
