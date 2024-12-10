using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.ServiceManagement.Entities;

public class ServiceAvailability : EntityBase<int>
{
    public required int ServiceId { get; set; }
    public required int ServiceResourceId { get; set; }
    public required int Priority { get; set; }
    
    public Service Service { get; set; }
    public ServiceResource ServiceResource { get; set; }
}