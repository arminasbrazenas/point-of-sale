using PointOfSale.Models.ServiceManagement.Enums;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.ServiceManagement.Entities;

public class Reservation : EntityBase<int>
{
    public required DateTime DateStart { get; set; }
    public required DateTime DateEnd { get; set; }
    public required ReservationStatus Status { get; set; }
    public required int ServiceResourceId { get; set; }
    public required int ContactInfoId { get; set; }
    public required int ServiceId { get; set; }
    public required int EmployeeId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
    
    public ContactInfo ContactInfo { get; set; }
    public Service Service { get; set; }
    
    public ServiceResource ServiceResource { get; set; }
}