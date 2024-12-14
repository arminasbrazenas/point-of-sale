namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record ContactInfoDTO
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
}