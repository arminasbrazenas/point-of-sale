namespace PointOfSale.BusinessLogic.Shared.DTOs;

public record PaginationFilterDTO
{
    public required int Page { get; init; }
    public required int ItemsPerPage { get; init; }
}
