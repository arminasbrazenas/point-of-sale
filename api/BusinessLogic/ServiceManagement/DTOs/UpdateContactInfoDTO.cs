namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record UpdateContactInfoDTO
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? PhoneNumber { get; init; }
}