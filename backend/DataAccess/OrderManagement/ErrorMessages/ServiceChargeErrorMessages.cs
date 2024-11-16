using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class ServiceChargeNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _serviceChargeId;

    public ServiceChargeNotFoundErrorMessage(int serviceChargeId)
    {
        _serviceChargeId = serviceChargeId;
    }

    public string En => $"Service charge with id '{_serviceChargeId}' not found.";
}
