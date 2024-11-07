using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.ErrorMessages;

public class PaginationFilterPageNotPositiveErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Pagination filter page must be greater than zero.";
}

public class PaginationFilterItemCountNotPositiveErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Pagination filter item count must be greater than zero.";
}

public class PaginationFilterItemCountTooBigErrorMessage : IPointOfSaleErrorMessage
{
    private int _maxCount;

    public PaginationFilterItemCountTooBigErrorMessage(int maxCount)
    {
        _maxCount = maxCount;
    }

    public string En => $"Pagination filter item count is too big. Max item count is {_maxCount}.";
}
