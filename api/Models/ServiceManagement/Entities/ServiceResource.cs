using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.ServiceManagement.Entities;

public class ServiceResource : EntityBase<int>
{
    public required string Name { get; set; }
    
    public List<ServiceAvailability> ServiceAvailability { get; set; }
    public List<Reservation> Reservations { get; set; }
}