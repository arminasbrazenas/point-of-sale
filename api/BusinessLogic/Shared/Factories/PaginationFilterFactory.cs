using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.ErrorMessages;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;

namespace PointOfSale.BusinessLogic.Shared.Factories;

public static class PaginationFilterFactory
{
    private const int MaxItemsPerPage = 50;

    public static PaginationFilter Create(PaginationFilterDTO paginationFilterDTO)
    {
        if (paginationFilterDTO.Page < 1)
        {
            throw new ValidationException(new PaginationFilterPageNotPositiveErrorMessage());
        }

        if (paginationFilterDTO.ItemsPerPage < 1)
        {
            throw new ValidationException(new PaginationFilterItemCountNotPositiveErrorMessage());
        }

        if (paginationFilterDTO.ItemsPerPage > MaxItemsPerPage)
        {
            throw new ValidationException(new PaginationFilterItemCountTooBigErrorMessage(MaxItemsPerPage));
        }

        return new PaginationFilter
        {
            Page = paginationFilterDTO.Page,
            ItemsPerPage = paginationFilterDTO.ItemsPerPage,
        };
    }
}
