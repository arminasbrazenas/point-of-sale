using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.Shared.ErrorMessages;

public class TextErrorMessage(string text) : IPointOfSaleErrorMessage
{
    public string En => text;
}
