using PointOfSale.DataAccess.Shared.ErrorMessages;
using PointOfSale.DataAccess.Shared.Exceptions;

namespace PointOfSale.DataAccess.Shared.DTOs;

public record PaginationFilter
{
    private const int MaxItemsPerPage = 50;
    
    public required int Page { get; init; }
    public required int ItemsPerPage { get; init; }

    private PaginationFilter()
    {
    }

    public static PaginationFilter Create(int page, int itemsPerPage)
    {
        if (page < 1)
        {
            throw new ValidationException(new PaginationFilterPageNotPositiveErrorMessage());
        }

        if (itemsPerPage < 1)
        {
            throw new ValidationException(new PaginationFilterItemCountNotPositiveErrorMessage());
        }

        if (itemsPerPage > MaxItemsPerPage)
        {
            throw new ValidationException(new PaginationFilterItemCountTooBigErrorMessage(MaxItemsPerPage));
        }

        return new PaginationFilter
        {
            Page = page,
            ItemsPerPage = itemsPerPage
        };
    }
}