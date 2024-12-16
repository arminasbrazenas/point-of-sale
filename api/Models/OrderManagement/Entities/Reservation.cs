using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.OrderManagement.ValueObjects;

namespace PointOfSale.Models.OrderManagement.Entities;

public class Reservation
{
    public Service Service { get; set; }
    public int ServiceId { get; set; }
    public required ReservationDate Date { get; set; }
    public required ReservationCustomer Customer { get; set; }
    public required int EmployeeId { get; set; }
    public ApplicationUser Employee { get; set; }
}