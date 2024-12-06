using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.BusinessManagement.ErrorMessages;

public class InvalidBusinessIdErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _id;

    public InvalidBusinessIdErrorMessage(int id)
    {
        _id = id;
    }

    public string En => $"Business with '{_id}' does not exist.";
}