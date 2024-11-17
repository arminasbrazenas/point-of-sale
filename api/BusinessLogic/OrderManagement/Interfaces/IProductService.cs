using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IProductService
{
    Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO);
    Task<ProductDTO> UpdateProduct(int productId, UpdateProductDTO updateProductDTO);
    Task<ProductDTO> GetProduct(int productId);
    Task<PagedResponseDTO<ProductDTO>> GetProducts(PaginationFilterDTO paginationFilterDTO);
    Task DeleteProduct(int productId);
    Task SetProductModifiers(int productId, SetModifiersForProductDTO setModifiersForProductDTO);
    Task<PagedResponseDTO<ModifierDTO>> GetProductModifiers(int productId, PaginationFilterDTO paginationFilterDTO);
}