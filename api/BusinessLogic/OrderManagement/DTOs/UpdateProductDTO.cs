namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateProductDTO
{
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public int? Stock { get; init; }
    public List<int>? TaxIds { get; init; }
}
