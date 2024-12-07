using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.BusinessManagement.ErrorMessages;

public class BusinessNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _businessId;

    public BusinessNotFoundErrorMessage(int businessId)
    {
        _businessId = businessId;
    }

    public string En => $"Business with id '{_businessId}' not found.";
}
