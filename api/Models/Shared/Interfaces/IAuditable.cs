namespace PointOfSale.Models.Shared.Interfaces;

public interface IAuditable
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}
