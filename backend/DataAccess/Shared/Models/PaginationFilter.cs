namespace PointOfSale.DataAccess.Shared.Models;

public record PaginationFilter
{
    public required int Page { get; init; }
    public required int ItemsPerPage { get; init; }
}
