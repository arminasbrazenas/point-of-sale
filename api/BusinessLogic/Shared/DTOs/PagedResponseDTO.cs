namespace PointOfSale.BusinessLogic.Shared.DTOs;

public record PagedResponseDTO<T>
{
    public required int Page { get; init; }
    public required int ItemsPerPage { get; init; }
    public required int TotalItems { get; set; }
    public required List<T> Items { get; init; }
}
