using PointOfSale.BusinessLogic.OrderManagement.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IDiscountService
{
    Task<DiscountDTO> CreateDiscount(CreateDiscountDTO createDiscountDTO);
}