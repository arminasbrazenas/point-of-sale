using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.ServiceManagement.ErrorMessages;

public class ServiceResourceNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service resource name cannot be empty.";
}

public class ServiceResourceNameLengthErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public ServiceResourceNameLengthErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }
    public string En => $"Service resource name cannot be more than {_maxLength} characters long.";
}

public class ServiceResourceExistsErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Service resource with this name already exists.";
}