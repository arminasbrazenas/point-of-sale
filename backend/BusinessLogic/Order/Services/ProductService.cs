using PointOfSale.BusinessLogic.Order.DTOs;
using PointOfSale.BusinessLogic.Order.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.Order.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductValidator _productValidator;

    public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, IProductValidator productValidator)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productValidator = productValidator;
    }

    public async Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO)
    {
        var name = await _productValidator.ValidateName(createProductDTO.Name);
        var price = _productValidator.ValidatePrice(createProductDTO.Price);
        var stock = _productValidator.ValidateStock(createProductDTO.Stock);
        var taxes = await _productValidator.ValidateTaxes(createProductDTO.TaxIds);

        var product = new Product
        {
            Name = name,
            Price = price,
            Stock = stock,
            Taxes = taxes,
        };

        _productRepository.Add(product);
        await _unitOfWork.SaveChanges();

        return ProductDTO.Create(product);
    }

    public async Task<ProductDTO> UpdateProduct(int productId, UpdateProductDTO updateProductDTO)
    {
        var product = await _productRepository.GetWithTaxes(productId);
        
        if (updateProductDTO.Name is not null)
        {
            product.Name = await _productValidator.ValidateName(updateProductDTO.Name);
        }

        if (updateProductDTO.Price.HasValue)
        {
            product.Price = _productValidator.ValidatePrice(updateProductDTO.Price.Value);
        }

        if (updateProductDTO.Stock.HasValue)
        {
            product.Stock = _productValidator.ValidateStock(updateProductDTO.Stock.Value);
        }

        if (updateProductDTO.TaxIds is not null)
        {
            product.Taxes = await _productValidator.ValidateTaxes(updateProductDTO.TaxIds);
        }
        
        _productRepository.Update(product);
        await _unitOfWork.SaveChanges();

        return ProductDTO.Create(product);
    }

    public async Task<ProductDTO> GetProduct(int productId)
    {
        var product = await _productRepository.GetWithTaxes(productId);
        return ProductDTO.Create(product);
    }

    public async Task<PaginatedResponseDTO<ProductDTO>> GetProducts(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilter.Create(paginationFilterDTO.Page, paginationFilterDTO.ItemsPerPage);
        var products = await _productRepository.GetPaginatedWithTaxes(paginationFilter);
        
        return new PaginatedResponseDTO<ProductDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            Items = products.Select(ProductDTO.Create).ToList(),
        };
    }

    public async Task DeleteProduct(int productId)
    {
        await _productRepository.Delete(productId);
    }
}
