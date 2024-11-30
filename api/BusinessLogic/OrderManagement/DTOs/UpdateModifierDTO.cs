namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateModifierDTO
{
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public int? Stock { get; init; }
}
