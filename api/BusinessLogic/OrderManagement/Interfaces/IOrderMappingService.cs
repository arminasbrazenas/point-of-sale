using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderMappingService
{
    OrderMinimalDTO MapToOrderMinimalDTO(Order order);
    PagedResponseDTO<OrderMinimalDTO> MapToPagedOrderMinimalDTO(List<Order> orders, PaginationFilter paginationFilter);
    OrderDTO MapToOrderDTO(Order order);
}
