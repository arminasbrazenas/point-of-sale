namespace PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;

public sealed record RegisterApplicationUserDTO(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    int? BusinessId,
    string Role
);
