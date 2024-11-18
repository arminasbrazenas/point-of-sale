namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateTaxDTO
{
    public string? Name { get; init; }
    public decimal? Rate { get; init; }
}
