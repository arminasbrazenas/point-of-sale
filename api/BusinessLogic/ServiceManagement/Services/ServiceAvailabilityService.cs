using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceAvailabilityService : IServiceAvailabilityService
{
    private readonly IServiceAvailabilityRepository _serviceAvailabilityRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceAvailabilityValidationService _serviceAvailabilityValidationService;
    private readonly IServiceAvailabilityMappingService _serviceAvailabilityMappingService;

    public ServiceAvailabilityService(
        IServiceAvailabilityMappingService serviceAvailabilityMappingService,
        IUnitOfWork unitOfWork,
        IServiceAvailabilityRepository serviceAvailabilityRepository,
        IServiceAvailabilityValidationService serviceAvailabilityValidationService)
    {
        _serviceAvailabilityMappingService = serviceAvailabilityMappingService;
        _unitOfWork = unitOfWork;
        _serviceAvailabilityRepository = serviceAvailabilityRepository;
        _serviceAvailabilityValidationService = serviceAvailabilityValidationService;
    }

    public async Task<ServiceAvailabilityDTO> CreateServiceAvailability(CreateServiceAvailabilityDTO createServiceAvailabilityDto)
    {
        var serviceId = createServiceAvailabilityDto.ServiceId;
        var serviceResourceId = createServiceAvailabilityDto.ServiceResourceId;
        var priority = _serviceAvailabilityValidationService.ValidatePriority(createServiceAvailabilityDto.Priority);
        
        var serviceAvailability = new ServiceAvailability{ServiceId = serviceId, ServiceResourceId = serviceResourceId, Priority = priority};
        
        _serviceAvailabilityRepository.Add(serviceAvailability);
        await _unitOfWork.SaveChanges();

        return _serviceAvailabilityMappingService.MapToServiceAvailabilityDTO(serviceAvailability);

    }

    public async Task<ServiceAvailabilityDTO> UpdateServiceAvailability(int serviceAvailabilityId, UpdateServiceAvailabilityDTO updateServiceAvailabilityDto)
    {
        var serviceAvailability = await _serviceAvailabilityRepository.Get(serviceAvailabilityId);

        if (updateServiceAvailabilityDto.ServiceId is not null)
        {
            serviceAvailability.ServiceId = updateServiceAvailabilityDto.ServiceId.Value;
        }

        if (updateServiceAvailabilityDto.ServiceResourceId is not null)
        {
            serviceAvailability.ServiceResourceId = updateServiceAvailabilityDto.ServiceResourceId.Value;
        }

        if (updateServiceAvailabilityDto.Priority is not null)
        {
            serviceAvailability.Priority = _serviceAvailabilityValidationService.ValidatePriority(updateServiceAvailabilityDto.Priority.Value);
        }
        
        _serviceAvailabilityRepository.Update(serviceAvailability);
        await _unitOfWork.SaveChanges();
        
        return _serviceAvailabilityMappingService.MapToServiceAvailabilityDTO(serviceAvailability);
    }

    public async Task DeleteServiceAvailability(int serviceAvailabilityId)
    {
        await _serviceAvailabilityRepository.Delete(serviceAvailabilityId);
    }

    public async Task<ServiceAvailabilityDTO> GetServiceAvailability(int serviceAvailabilityId)
    {
        var serviceAvailability = await _serviceAvailabilityRepository.Get(serviceAvailabilityId);
        return _serviceAvailabilityMappingService.MapToServiceAvailabilityDTO(serviceAvailability);
    }
}