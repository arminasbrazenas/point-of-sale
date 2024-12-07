using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Extensions;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ProductMappingService : IProductMappingService
{
    private readonly ITaxMappingService _taxMappingService;
    private readonly IModifierMappingService _modifierMappingService;

    public ProductMappingService(ITaxMappingService taxMappingService, IModifierMappingService modifierMappingService)
    {
        _taxMappingService = taxMappingService;
        _modifierMappingService = modifierMappingService;
    }

    public ProductDTO MapToProductDTO(Product product)
    {
        var basePrice = product.Price.ToRoundedPrice();
        var grossPrice = basePrice - product.Discounts.GetAmountToApply(basePrice);
        var netPrice = grossPrice + product.Taxes.GetAmountToApply(grossPrice);
        var netPriceDiscountExcluded =
            product.Discounts.Count > 0 ? basePrice + product.Taxes.GetAmountToApply(basePrice) : (decimal?)null;

        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            BasePrice = basePrice,
            PriceDiscountExcluded = netPriceDiscountExcluded,
            Price = netPrice,
            Stock = product.Stock,
            Taxes = product.Taxes.Select(t => _taxMappingService.MapToTaxDTO(t)).ToList(),
            Modifiers = product
                .Modifiers.Select(m => _modifierMappingService.MapToModifierDTO(m, product.Taxes))
                .ToList(),
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
