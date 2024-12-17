using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.OrderManagement;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ServiceValidationService : IServiceValidationService
{
    private readonly IServiceRepository _serviceRepository;
    
    public ServiceValidationService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    
    public async Task<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new ServiceNameEmptyErrorMessage());
        }

        if (name.Length > Constants.ServiceNameMaxLength)
        {
            throw new ValidationException(new ServiceNameLengthErrorMessage(Constants.ServiceNameMaxLength));
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