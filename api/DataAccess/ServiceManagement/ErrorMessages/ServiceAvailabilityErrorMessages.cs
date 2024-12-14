using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.ServiceManagement.ErrorMessages;

public class ServiceAvailabilityPriorityErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Priority cant be zero or negative.";
}