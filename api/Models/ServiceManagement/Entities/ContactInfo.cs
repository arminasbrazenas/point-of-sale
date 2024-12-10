using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.ServiceManagement.Entities;

public class ContactInfo : EntityBase<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    
    
    public List<Reservation> Reservations { get; set; }
}