using PointOfSale.DataAccess.Order.Entities;

namespace PointOfSale.BusinessLogic.Order.DTOs;

public record ProductDTO
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }

    public static ProductDTO Create(Product product)
    {
        return new ProductDTO
        {
            Name = product.Name,
            Price = product.Price + CalculateTaxTotal(product),
            Stock = product.Stock
        };
    }
    
    private static decimal CalculateTaxTotal(Product product)
    {
        return product.Taxes.Sum(tax => product.Price * tax.Rate);
    }
}