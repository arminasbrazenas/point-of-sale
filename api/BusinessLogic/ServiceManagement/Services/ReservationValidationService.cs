using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ServiceManagement.ErrorMessages;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ReservationValidationService : IReservationValidationService
{
    public DateTime ValidateCreateDateStart(DateTime date)
    {

        if (date < DateTime.Now)
        {
            throw new ValidationException(new ReservationCreateDateStartErrorMessage());
        }
        return date;
    }
    
}