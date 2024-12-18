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

public class ApplicationUserUnauthorizedErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Application user has no access to the content.";
}

public class ApplicationUserNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _id;

    public ApplicationUserNotFoundErrorMessage(int id)
    {
        _id = id;
    }

    public string En => $"No application user with id {_id} exists.";
}

public class FailedActionOnApplicationUserErrorMessage : IPointOfSaleErrorMessage
{
    private readonly string _errors;

    public FailedActionOnApplicationUserErrorMessage(string errors)
    {
        _errors = errors;
    }

    public string En => $"Action on ApplicationUser failed with errors: {_errors}";
}

public class InvalidEmailErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Invalid email.";
}

public class InvalidPhoneNumberErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Invalid phone number.";
}

public class InvalidRefreshTokenErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Invalid refresh token";
}
