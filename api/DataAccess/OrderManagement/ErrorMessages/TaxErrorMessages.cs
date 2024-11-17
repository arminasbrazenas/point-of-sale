using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class TaxNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _taxId;

    public TaxNotFoundErrorMessage(int taxId)
    {
        _taxId = taxId;
    }

    public string En => $"Tax with id '{_taxId}' not found.";
}

public class TaxesNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly List<int> _taxIds;

    public TaxesNotFoundErrorMessage(List<int> taxIds)
    {
        _taxIds = taxIds;
    }

    public string En => $"Tax with ids '{string.Join("; ", _taxIds)}' were not found.";
}