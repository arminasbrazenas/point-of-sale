using PointOfSale.BusinessLogic.Order.DTOs;

namespace PointOfSale.BusinessLogic.Order.Interfaces;

public interface IProductService
{
    Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO);
}