namespace PointOfSale.BusinessLogic.BusinessManagement.DTOs;

public record CreateBusinessDTO
{
    public required int BusinessOwnerId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required int StartMinute { get; set; }
    public required int StartHour { get; set; }
    public required int EndHour { get; set; }
    public required int EndMinute { get; set; }
}
