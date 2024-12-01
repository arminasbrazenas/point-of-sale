namespace PointOfSale.BusinessLogic.BusinessManagement.DTOs;

public record UpdateBusinessDTO
{
    public  int ? BusinessOwnerId { get; set; }
    public  string ? Name { get; set; }
    public  string ? Address { get; set; }
    public  string ? TelephoneNumber { get; set; }
    public  string ?Email { get; set; }
}
