using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.ServiceManagement.Entities;

public class Service : EntityBase<int>
{
    public required string Name { get; set; }
    public required DateTime AvailableFrom { get; set; }
    public required DateTime AvailableTo { get; set; }
    public required TimeSpan Duration { get; set; }
    public required decimal Price { get; set; }
    
    public List<Reservation> Reservations { get; set; }
    public List<ServiceAvailability> ServiceAvailability { get; set; }
}