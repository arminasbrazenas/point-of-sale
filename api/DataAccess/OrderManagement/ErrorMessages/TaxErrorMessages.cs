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

public class TaxNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Tax name cannot be empty.";
}

public class TaxNameTooLongErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public TaxNameTooLongErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }

    public string En => $"Tax name is too long. Max length is {_maxLength} characters.";
}

public class TaxNameConflictErrorMessage : IPointOfSaleErrorMessage
{
    private readonly string _name;

    public TaxNameConflictErrorMessage(string name)
    {
        _name = name;
    }

    public string En => $"Tax with name '{_name}' already exists.";
}

public class TaxRateInvalidErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Tax rate is invalid. It must be between 0 and 1.";
}
