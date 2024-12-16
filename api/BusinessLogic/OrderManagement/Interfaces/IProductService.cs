using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IProductService
{
    Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO);
    Task<ProductDTO> UpdateProduct(int productId, UpdateProductDTO updateProductDTO);
    Task<ProductDTO> GetProduct(int productId);
    Task<PagedResponseDTO<ProductDTO>> GetProducts(PaginationFilterDTO paginationFilterDTO, int businessId);
    Task DeleteProduct(int productId);
}
