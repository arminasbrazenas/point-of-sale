using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record TaxDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Rate { get; init; }

    public static TaxDTO Create(Tax tax)
    {
        return new TaxDTO
        {
            Id = tax.Id,
            Name = tax.Name,
            Rate = tax.Rate,
        };
    }
}
