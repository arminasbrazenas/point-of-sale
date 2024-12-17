using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceValidationService _serviceValidationService;
    private readonly IServiceMappingService _serviceMappingService;

    public ServiceService(
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork,
        IServiceValidationService serviceValidator,
        IServiceMappingService serviceMappingService)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
        _serviceValidationService = serviceValidator;
        _serviceMappingService = serviceMappingService;
    }

    public async Task<ServiceDTO> CreateService(CreateServiceDTO createServiceDto)
    {
        var name = await _serviceValidationService.ValidateName(createServiceDto.Name);
        var duration = createServiceDto.Duration;
        var price = _serviceValidationService.ValidatePrice(createServiceDto.Price);
        var businessId = createServiceDto.BusinessId;

        var service = new Service
        {
            Name = name,
            Duration = duration,
            Price = price,
            BusinessId = businessId
        };
        
        _serviceRepository.Add(service);
        await _unitOfWork.SaveChanges();
        
        return _serviceMappingService.MapToServiceDTO(service);
    }
    
    public async Task<ServiceDTO> UpdateService(int serviceId, UpdateServiceDTO updateServiceDto)
    {
        var service = await _serviceRepository.Get(serviceId);

        if (updateServiceDto.Name is not null)
        {
            service.Name = await _serviceValidationService.ValidateName(updateServiceDto.Name);
        }

        if (updateServiceDto.Duration is not null)
        {
            service.Duration = updateServiceDto.Duration.Value;
        }

        if (updateServiceDto.Price is not null)
        {
            service.Price = _serviceValidationService.ValidatePrice(updateServiceDto.Price.Value);   
        }
        
        _serviceRepository.Update(service);
        await _unitOfWork.SaveChanges();
        
        return _serviceMappingService.MapToServiceDTO(service);
    }

    public async Task DeleteService(int serviceId)
    {
        await _serviceRepository.Delete(serviceId);
    }

    public async Task<ServiceDTO> GetService(int serviceId)
    {
        var service = await _serviceRepository.Get(serviceId);
        return _serviceMappingService.MapToServiceDTO(service);
    }
    
    public async Task<PagedResponseDTO<ServiceDTO>> GetServices(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var services = await _serviceRepository.GetPaged(paginationFilter);
        var totalCount = await _serviceRepository.GetTotalCount();
        return _serviceMappingService.MapToPagedServiceDTO(services, paginationFilter, totalCount);
    }
}