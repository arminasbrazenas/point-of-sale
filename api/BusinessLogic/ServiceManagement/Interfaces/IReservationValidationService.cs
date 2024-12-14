namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IReservationValidationService
{
    public DateTime ValidateCreateDateStart(DateTime date);
}