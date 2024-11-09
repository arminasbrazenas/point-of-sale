using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductValidationService _productValidationService;
    private readonly IProductMappingService _productMappingService;

    public ProductService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IProductValidationService productValidationService,
        IProductMappingService productMappingService
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productValidationService = productValidationService;
        _productMappingService = productMappingService;
    }

    public async Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO)
    {
        var name = await _productValidationService.ValidateName(createProductDTO.Name);
        var price = _productValidationService.ValidatePrice(createProductDTO.Price);
        var stock = _productValidationService.ValidateStock(createProductDTO.Stock);
        var taxes = await _productValidationService.ValidateTaxes(createProductDTO.TaxIds);

        var product = new Product
        {
            Name = name,
            Price = price,
            Stock = stock,
            Taxes = taxes,
        };

        _productRepository.Add(product);
        await _unitOfWork.SaveChanges();

        return _productMappingService.MapToProductDTO(product);
    }

    public async Task<ProductDTO> UpdateProduct(int productId, UpdateProductDTO updateProductDTO)
    {
        var product = await _productRepository.GetWithTaxes(productId);

        if (updateProductDTO.Name is not null)
        {
            product.Name = await _productValidationService.ValidateName(updateProductDTO.Name);
        }

        if (updateProductDTO.Price.HasValue)
        {
            product.Price = _productValidationService.ValidatePrice(updateProductDTO.Price.Value);
        }

        if (updateProductDTO.Stock.HasValue)
        {
            product.Stock = _productValidationService.ValidateStock(updateProductDTO.Stock.Value);
        }

        if (updateProductDTO.TaxIds is not null)
        {
            product.Taxes = await _productValidationService.ValidateTaxes(updateProductDTO.TaxIds);
        }

        _productRepository.Update(product);
        await _unitOfWork.SaveChanges();

        return _productMappingService.MapToProductDTO(product);
    }

    public async Task<ProductDTO> GetProduct(int productId)
    {
        var product = await _productRepository.GetWithTaxes(productId);
        return _productMappingService.MapToProductDTO(product);
    }

    public async Task<PagedResponseDTO<ProductDTO>> GetProducts(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var products = await _productRepository.GetPaginatedWithTaxes(paginationFilter);
        return _productMappingService.MapToPagedProductDTO(products, paginationFilter);
    }

    public async Task DeleteProduct(int productId)
    {
        await _productRepository.Delete(productId);
    }
}
