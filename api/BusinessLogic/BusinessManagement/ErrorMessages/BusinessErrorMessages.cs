using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.BusinessManagement.ErrorMessages;

public class InvalidBusinessIdErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _id;

    public InvalidBusinessIdErrorMessage(int id)
    {
        _id = id;
    }

    public string En => $"Business with '{_id}' does not exist.";
}

public class UnauthorizedAccessToBusinessErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _id;

    public UnauthorizedAccessToBusinessErrorMessage(int id)
    {
        _id = id;
    }

    public string En => $"Unauthorized access to business with id {_id}";
}

public class InvalidApplicationUserRoleToOwnBusinessErrorMessage : IPointOfSaleErrorMessage
{
    public string En => $"Only users with role BusinessOwner can own business.";
}

public class ApplicationUserCannotOwnMultipleBusinessesErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _id;

    public ApplicationUserCannotOwnMultipleBusinessesErrorMessage(int id)
    {
        _id = id;
    }

    public string En => $"Business owner with {_id} cannot own multiple businesses.";
}

public class InvalidWorkingHoursErrorMessage : IPointOfSaleErrorMessage
{
    public string En => $"Provided working hours are invalid.";
}
