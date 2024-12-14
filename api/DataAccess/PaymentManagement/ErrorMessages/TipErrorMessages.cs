using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.PaymentManagement.ErrorMessages;

public class TipNotFoundErrorMessage(int tipId) : IPointOfSaleErrorMessage
{
    public string En => $"Tip with id {tipId} not found.";
}
