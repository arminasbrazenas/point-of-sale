using PointOfSale.DataAccess.Order.Entities;

namespace PointOfSale.BusinessLogic.Order.DTOs;

public record TaxDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Rate { get; init; }

    public static TaxDTO FromEntity(Tax tax)
    {
        return new TaxDTO
        {
            Id = tax.Id,
            Name = tax.Name,
            Rate = tax.Rate
        };
    }
}