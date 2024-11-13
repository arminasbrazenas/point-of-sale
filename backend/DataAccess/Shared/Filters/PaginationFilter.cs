namespace PointOfSale.DataAccess.Shared.Filters;

public record PaginationFilter
{
    public required int Page { get; init; }
    public required int ItemsPerPage { get; init; }
}
