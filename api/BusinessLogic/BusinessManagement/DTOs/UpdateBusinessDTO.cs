namespace PointOfSale.BusinessLogic.BusinessManagement.DTOs;

public record UpdateBusinessDTO
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public int? StartMinute { get; set; }
    public int? StartHour { get; set; }
    public int? EndHour { get; set; }
    public int? EndMinute { get; set; }
}
