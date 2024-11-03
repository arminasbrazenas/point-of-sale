using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Order.ErrorMessages;

public class TaxNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _taxId;

    public TaxNotFoundErrorMessage(int taxId)
    {
        _taxId = taxId;
    }

    public string En => $"Tax with id '{_taxId}' not found.";
}
