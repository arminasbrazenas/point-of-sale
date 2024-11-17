using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ProductMappingService : IProductMappingService
{
    public ProductDTO MapToProductDTO(Product product)
    {
        var taxRates = product.Taxes.Select(t => t.Rate);
        var price = product.Price + PriceUtility.CalculateTotalTax(product.Price, taxRates);

        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = price.ToRoundedPrice(),
            Stock = product.Stock,
        };
    }

    public PagedResponseDTO<ProductDTO> MapToPagedProductDTO(
        List<Product> products,
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<ProductDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Items = products.Select(MapToProductDTO).ToList(),
        };
    }
}
