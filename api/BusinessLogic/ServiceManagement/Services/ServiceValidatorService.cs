using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ServiceManagement;
using PointOfSale.DataAccess.ServiceManagement.ErrorMessages;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceValidatorService : IServiceValidatorService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceValidatorService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    
    public async Task<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new ServiceNameEmptyErrorMessage());
        }

        if (name.Length > Constants.ServiceResourceNameLength)
        {
            throw new ValidationException(new ServiceNameLengthErrorMessage(Constants.ServiceNameLength));
        }
        
        var existingService = await _serviceRepository.GetServiceByName(name);
        if (existingService is not null)
        {
            throw new ValidationException(new ServiceExistsErrorMessage());
        }
        return name;
    }

    public decimal ValidatePrice(decimal price)
    {
        if (price <= 0m)
        {
            throw new ValidationException(new ServicePriceErrorMessage());
        }
        return price;
    }
}