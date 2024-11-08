using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IProductService
{
    Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO);
    Task<ProductDTO> UpdateProduct(int productId, UpdateProductDTO updateProductDTO);
    Task<ProductDTO> GetProduct(int productId);
    Task<PaginatedResponseDTO<ProductDTO>> GetProducts(PaginationFilterDTO paginationFilterDTO);
    Task DeleteProduct(int productId);
}
