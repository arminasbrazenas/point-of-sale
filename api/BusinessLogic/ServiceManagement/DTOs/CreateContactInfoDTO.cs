namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record CreateContactInfoDTO
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
}