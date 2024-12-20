using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class ServiceChargeNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _serviceChargeId;

    public ServiceChargeNotFoundErrorMessage(int serviceChargeId)
    {
        _serviceChargeId = serviceChargeId;
    }

    public string En => $"Service charge with id '{_serviceChargeId}' not found.";
}

public class ServiceChargeNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service charge name must not be empty.";
}

public class ServiceChargeNameTooLongErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service charge name is too long.";
}

public class ServiceChargeNameConflictErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service charge with such name already exists.";
}

public class ServiceChargeAmountNegativeErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service charge amount cannot be negative.";
}

public class ServiceChargeInvalidPercentageErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service charge percentage must be between 0 and 100.";
}

public class CustomerFirstNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "The customer's first name cannot be empty";
}

public class CustomerFirstNameLengthErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public CustomerFirstNameLengthErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }

    public string En => $"The customer's first name cannot be more than {_maxLength} characters long.";
}

public class CustomerLastNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "The customer's last name cannot be empty.";
}

public class CustomerPhoneNumberEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "The customer's phone number cannot be empty.";
}

public class InvalidPhoneNumberErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Phone number is invalid.";
}

public class CustomerLastNameLengthErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public CustomerLastNameLengthErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }

    public string En => $"The customer's last name cannot be more than {_maxLength} characters long.";
}
