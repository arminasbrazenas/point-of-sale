using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceResourceService : IServiceResourceService
{
    private readonly IServiceResourceRepository _serviceResourceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceResourcesMappingService _serviceResourcesMappingService;
    private readonly IServiceResourceValidatorService _serviceResourceValidatorService;

    public ServiceResourceService(
        IServiceResourceRepository serviceResourceRepository,
        IUnitOfWork unitOfWork,
        IServiceResourcesMappingService serviceResourcesMappingService,
        IServiceResourceValidatorService serviceResourceValidatorService)
    {
        _serviceResourceRepository = serviceResourceRepository;
        _unitOfWork = unitOfWork;
        _serviceResourcesMappingService = serviceResourcesMappingService;
        _serviceResourceValidatorService = serviceResourceValidatorService;
    }

    public async Task<ServiceResourceDTO> CreateServiceResource(CreateServiceResourceDTO createServiceResourceDto)
    {
        var name = await _serviceResourceValidatorService.ValidateName(createServiceResourceDto.Name);
        
        var serviceResource = new ServiceResource { Name = name };
        
        _serviceResourceRepository.Add(serviceResource);
        await _unitOfWork.SaveChanges();
        
        return _serviceResourcesMappingService.MapToServiceResourceDTO(serviceResource);
    }

    public async Task<ServiceResourceDTO> UpdateServiceResource(int serviceResourceId, UpdateServiceResourceDTO updateServiceResourceDto)
    {
        var serviceResource = await _serviceResourceRepository.Get(serviceResourceId);

        if (updateServiceResourceDto.Name is not null)
        {
            serviceResource.Name = await _serviceResourceValidatorService.ValidateName(updateServiceResourceDto.Name);
        }
        
        _serviceResourceRepository.Update(serviceResource);
        await _unitOfWork.SaveChanges();
        
        return _serviceResourcesMappingService.MapToServiceResourceDTO(serviceResource);
    }

    public async Task DeleteServiceResource(int serviceResourceId)
    {
        await _serviceResourceRepository.Delete(serviceResourceId);
    }

    public async Task<ServiceResourceDTO> GetServiceResource(int serviceResourceId)
    {
        var serviceResource = await _serviceResourceRepository.Get(serviceResourceId);
        return _serviceResourcesMappingService.MapToServiceResourceDTO(serviceResource);
    }
}