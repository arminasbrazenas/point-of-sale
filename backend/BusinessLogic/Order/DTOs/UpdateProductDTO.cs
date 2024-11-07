namespace PointOfSale.BusinessLogic.Order.DTOs;

public record UpdateProductDTO
{
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public int? Stock { get; set; }
    public List<int>? TaxIds { get; init; }
}