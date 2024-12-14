using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceValidatorService _serviceValidationService;
    private readonly IServiceMappingService _serviceMappingService;

    public ServiceService(
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork,
        IServiceValidatorService serviceValidator,
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
        var availableFrom = createServiceDto.AvailableFrom;
        var availableTo = createServiceDto.AvailableTo;
        var duration = createServiceDto.Duration;
        var price = _serviceValidationService.ValidatePrice(createServiceDto.Price);

        var service = new Service
        {
            Name = name,
            AvailableFrom = availableFrom,
            AvailableTo = availableTo,
            Duration = duration,
            Price = price
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

        if (updateServiceDto.AvailableFrom is not null)
        {
            service.AvailableFrom = updateServiceDto.AvailableFrom.Value;
        }

        if (updateServiceDto.AvailableTo is not null)
        {
            service.AvailableTo = updateServiceDto.AvailableTo.Value;
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
}