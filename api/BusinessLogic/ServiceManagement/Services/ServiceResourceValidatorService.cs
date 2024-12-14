using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.ServiceManagement;
using PointOfSale.DataAccess.ServiceManagement.ErrorMessages;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using ValidationException = PointOfSale.BusinessLogic.Shared.Exceptions.ValidationException;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceResourceValidatorService : IServiceResourceValidatorService
{
    private readonly IServiceResourceRepository _serviceResourceRepository;

    public ServiceResourceValidatorService(IServiceResourceRepository serviceResourceRepository)
    {
        _serviceResourceRepository = serviceResourceRepository;
    }
    
    public async Task<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new ServiceResourceNameEmptyErrorMessage());
        }

        if (name.Length > Constants.ServiceResourceNameLength)
        {
            throw new ValidationException(new ServiceResourceNameLengthErrorMessage(Constants.ServiceResourceNameLength));
        }
        var existingRecourse = await _serviceResourceRepository.GetServiceResourceByName(name);
        if (existingRecourse is not null)
        {
            throw new ValidationException(new ServiceResourceExistsErrorMessage());
        }
        
        return name;
    }
}