using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.ServiceManagement.ErrorMessages;

public class ServiceNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service resource name cannot be empty.";
}

public class ServiceNameLengthErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public ServiceNameLengthErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }
    public string En => $"Service name cannot be more than {_maxLength} characters long.";
}

public class ServicePriceErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service price cannot be less than 0.";
}

public class ServiceExistsErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service with this name already exists.";
}