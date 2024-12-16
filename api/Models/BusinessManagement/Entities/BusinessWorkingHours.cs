namespace PointOfSale.Models.BusinessManagement.Entities;

public record BusinessWorkingHours
{
    public required TimeOnly Start { get; set; }
    public required TimeOnly End { get; set; }
}