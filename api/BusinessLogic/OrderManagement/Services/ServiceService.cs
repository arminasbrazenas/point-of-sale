using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
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
    private readonly IOrderManagementAuthorizationService _orderManagementAuthorizationService;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public ServiceService(
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork,
        IServiceValidationService serviceValidator,
        IServiceMappingService serviceMappingService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService,
        IApplicationUserRepository applicationUserRepository
    )
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
        _serviceValidationService = serviceValidator;
        _serviceMappingService = serviceMappingService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<ServiceDTO> CreateService(CreateServiceDTO createServiceDto)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(createServiceDto.BusinessId);

        var name = await _serviceValidationService.ValidateName(createServiceDto.Name, createServiceDto.BusinessId);
        var durationInMinutes = _serviceValidationService.ValidateDurationInMinutes(createServiceDto.DurationInMinutes);
        var price = _serviceValidationService.ValidatePrice(createServiceDto.Price.ToRoundedPrice());
        var providedByEmployees = await _applicationUserRepository.GetManyByIdsAsync(
            createServiceDto.ProvidedByEmployeesWithId
        );

        var service = new Service
        {
            Name = name,
            Duration = TimeSpan.FromMinutes(durationInMinutes),
            Price = price,
            BusinessId = createServiceDto.BusinessId,
            ProvidedByEmployees = providedByEmployees,
        };

        _serviceRepository.Add(service);
        await _unitOfWork.SaveChanges();

        return _serviceMappingService.MapToServiceDTO(service);
    }

    public async Task<ServiceDTO> UpdateService(int serviceId, UpdateServiceDTO updateServiceDto)
    {
        var service = await _serviceRepository.GetWithRelatedData(serviceId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(service.BusinessId);

        if (updateServiceDto.Name is not null)
        {
            service.Name = await _serviceValidationService.ValidateName(updateServiceDto.Name, service.BusinessId);
        }

        if (updateServiceDto.DurationInMinutes.HasValue)
        {
            _serviceValidationService.ValidateDurationInMinutes(updateServiceDto.DurationInMinutes.Value);
            service.Duration = TimeSpan.FromMinutes(updateServiceDto.DurationInMinutes.Value);
        }

        if (updateServiceDto.Price.HasValue)
        {
            service.Price = _serviceValidationService.ValidatePrice(updateServiceDto.Price.Value.ToRoundedPrice());
        }

        if (updateServiceDto.ProvidedByEmployeesWithId is not null)
        {
            service.ProvidedByEmployees = await _applicationUserRepository.GetManyByIdsAsync(
                updateServiceDto.ProvidedByEmployeesWithId
            );
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
        var service = await _serviceRepository.GetWithRelatedData(serviceId);
        return _serviceMappingService.MapToServiceDTO(service);
    }

    public async Task<PagedResponseDTO<ServiceDTO>> GetServices(PaginationFilterDTO paginationFilterDTO, int businessId)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var services = await _serviceRepository.GetPaged(paginationFilter, businessId);
        var totalCount = await _serviceRepository.GetTotalCount(businessId);
        return _serviceMappingService.MapToPagedServiceDTO(services, paginationFilter, totalCount);
    }
}
