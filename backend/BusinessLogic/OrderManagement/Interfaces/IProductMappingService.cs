using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IProductMappingService
{
    ProductDTO MapToProductDTO(Product product);
    PagedResponseDTO<ProductDTO> MapToPagedProductDTO(List<Product> products, PaginationFilter paginationFilter);
}
