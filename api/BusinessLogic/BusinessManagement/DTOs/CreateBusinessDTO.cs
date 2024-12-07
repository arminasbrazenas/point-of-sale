namespace PointOfSale.BusinessLogic.BusinessManagement.DTOs;

public record CreateBusinessDTO
{
    public required int BusinessOwnerId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string TelephoneNumber { get; set; }
    public required string Email { get; set; }
}
