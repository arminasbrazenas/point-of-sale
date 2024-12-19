using PointOfSale.BusinessLogic.OrderManagement.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IReservationValidationService
{
    DateTimeOffset ValidateDateStart(DateTimeOffset date);
    string ValidateFirstName(string firstName);
    string ValidateLastName(string lastName);
    string ValidatePhoneNumber(string phoneNumber);
    int ValidateEmployeeId(ServiceDTO service, int employeeId);

    void ValidateWorkHours(
        TimeOnly workStart,
        TimeOnly workEnd,
        DateTimeOffset reservationStart,
        DateTimeOffset reservationEnd
    );
}
