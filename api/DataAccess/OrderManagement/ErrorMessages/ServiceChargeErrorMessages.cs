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

public class CustomerLastNameLengthErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public CustomerLastNameLengthErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }
    public string En => $"The customer's last name cannot be more than {_maxLength} characters long.";
}