using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;

public class InvalidRoleErrorMessage : IPointOfSaleErrorMessage
{
    private readonly string _role;

    public InvalidRoleErrorMessage(string role)
    {
        _role = role;
    }

    public string En => $"Role '{_role}' does not exist.";
}

public class InvalidBusinessOwnerIdErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _id;

    public InvalidBusinessOwnerIdErrorMessage(int id)
    {
        _id = id;
    }

    public string En => $"Business owner with id '{_id}' does not exist.";
}

public class InvalidApplicationUserCredentialsErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Application user email or password is incorrect.";
}

public class NoApplicationUserIdErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Cannot access application user id.";
}

public class NoApplicationUserRoleErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Cannot access application user role.";
}
