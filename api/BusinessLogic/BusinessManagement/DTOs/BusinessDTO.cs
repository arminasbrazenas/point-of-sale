namespace PointOfSale.BusinessLogic.BusinessManagement.DTOs;

public record BusinessDTO
{
    public required int Id { get; init; }
    public required int BusinessOwnerId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string TelephoneNumber { get; set; }
    public required string Email { get; set; }
}
