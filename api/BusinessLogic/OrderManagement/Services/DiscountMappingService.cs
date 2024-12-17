using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class DiscountMappingService : IDiscountMappingService
{
    public DiscountDTO MapToDiscountDTO(Discount discount)
    {
        return new DiscountDTO
        {
            Id = discount.Id,
            Amount = discount.Amount,
            PricingStrategy = discount.PricingStrategy,
            AppliesToProductIds = discount.AppliesTo.Select(p => p.Id).ToList(),
            ValidUntil = discount.ValidUntil,
            Target = discount.Target,
            BusinessId = discount.BusinessId,
        };
    }

    public PagedResponseDTO<DiscountDTO> MapToPagedDiscountDTO(
        List<Discount> discounts,
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<DiscountDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Items = discounts.Select(MapToDiscountDTO).ToList(),
        };
    }
}
