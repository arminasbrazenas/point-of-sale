using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ServiceManagement.ErrorMessages;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ServiceAvailabilityValidationService : IServiceAvailabilityValidationService
{
    public int ValidatePriority(int priority)
    {

        if (priority < 1)
        {
            throw new ValidationException(new ServiceAvailabilityPriorityErrorMessage());
        }
        return priority;
    }
}