using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.ServiceManagement.ErrorMessages;

public class ReservationCreateDateStartErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "This reservation time has already passed.";
}

public class ReservationDateEndErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Reservation end time cannot be earlier than reservation start time.";
}

public class ReservationResourceErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "This service cannot be reserved at this time.";
}
