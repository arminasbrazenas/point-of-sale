namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IReservationValidationService
{
    DateTimeOffset ValidateDateStart(DateTimeOffset date);
    string ValidateFirstName(string firstName);
    string ValidateLastName(string lastName);
    string ValidatePhoneNumber(string phoneNumber);
}
