using PhoneNumbers;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.OrderManagement;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ReservationValidationService : IReservationValidationService
{
    public DateTimeOffset ValidateDateStart(DateTimeOffset date)
    {
        if (date < DateTimeOffset.Now)
        {
            throw new ValidationException(new ReservationDateStartErrorMessage());
        }

        return date;
    }

    public string ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ValidationException(new CustomerFirstNameEmptyErrorMessage());
        }

        if (firstName.Length > Constants.CustomerFirstNameMaxLength)
        {
            throw new ValidationException(
                new CustomerFirstNameLengthErrorMessage(Constants.CustomerFirstNameMaxLength)
            );
        }

        return firstName;
    }

    public string ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ValidationException(new CustomerLastNameEmptyErrorMessage());
        }

        if (lastName.Length > Constants.CustomerLastNameMaxLength)
        {
            throw new ValidationException(new CustomerLastNameLengthErrorMessage(Constants.CustomerLastNameMaxLength));
        }

        return lastName;
    }

    public string ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ValidationException(new CustomerPhoneNumberEmptyErrorMessage());
        }

        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            phoneNumberUtil.Parse(phoneNumber, defaultRegion: null);
        }
        catch (NumberParseException)
        {
            throw new ValidationException(new InvalidPhoneNumberErrorMessage());
        }

        return phoneNumber;
    }

    public int ValidateEmployeeId(ServiceDTO service, int employeeId)
    {
        if (!service.ProvidedByEmployees.Any(e => e.Id == employeeId))
        {
            throw new ValidationException(new EmployeeDoesNotProvideSelectedServiceErrorMessage());
        }

        return employeeId;
    }
}
