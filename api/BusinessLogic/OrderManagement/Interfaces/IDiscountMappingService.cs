using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IDiscountMappingService
{
    DiscountDTO MapToDiscountDTO(Discount discount);
}