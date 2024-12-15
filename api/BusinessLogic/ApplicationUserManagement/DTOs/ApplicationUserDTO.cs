namespace PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;

public sealed record ApplicationUserDTO(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string phoneNumber,
    int? BusinessId,
    string? BusinessName,
    string Role
);
