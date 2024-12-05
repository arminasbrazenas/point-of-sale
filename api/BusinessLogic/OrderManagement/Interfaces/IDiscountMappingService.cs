using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IDiscountMappingService
{
    DiscountDTO MapToDiscountDTO(Discount discount);
    PagedResponseDTO<DiscountDTO> MapToPagedDiscountDTO(
        List<Discount> discounts,
        PaginationFilter paginationFilter,
        int totalCount
    );
}
