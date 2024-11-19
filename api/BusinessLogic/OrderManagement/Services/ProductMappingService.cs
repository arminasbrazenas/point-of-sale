using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ProductMappingService : IProductMappingService
{
    private readonly ITaxMappingService _taxMappingService;

    public ProductMappingService(ITaxMappingService taxMappingService)
    {
        _taxMappingService = taxMappingService;
    }
    
    public ProductDTO MapToProductDTO(Product product)
    {
        var priceWithoutTaxes = product.Price;
        var taxRates = product.Taxes.Select(t => t.Rate);
        var price = priceWithoutTaxes + PriceUtility.CalculateTotalTax(priceWithoutTaxes, taxRates);

        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            PriceWithoutTaxes = priceWithoutTaxes.ToRoundedPrice(),
            PriceWithTaxes = price.ToRoundedPrice(),
            Stock = product.Stock,
            Taxes = product.Taxes.Select(t => _taxMappingService.MapToTaxDTO(t)).ToList(),
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
