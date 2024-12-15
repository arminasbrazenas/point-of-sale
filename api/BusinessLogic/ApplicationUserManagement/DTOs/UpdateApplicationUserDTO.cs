namespace PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;

public sealed record UpdateApplicationUserDTO(
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    string? Password
);
