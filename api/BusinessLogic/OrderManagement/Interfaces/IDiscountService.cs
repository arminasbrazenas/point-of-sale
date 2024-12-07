using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IDiscountService
{
    Task<DiscountDTO> CreateDiscount(CreateDiscountDTO createDiscountDTO);
    Task<DiscountDTO> GetDiscount(int discountId);
    Task<PagedResponseDTO<DiscountDTO>> GetDiscounts(PaginationFilterDTO paginationFilterDTO);
    Task<DiscountDTO> UpdateDiscount(int discountId, UpdateDiscountDTO updateDiscountDTO);
    Task DeleteDiscount(int discountId);
}
