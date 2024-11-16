using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IModifierRepository _modifierRepository;
    private readonly IProductValidationService _productValidationService;
    private readonly IProductMappingService _productMappingService;
    private readonly IModifierMappingService _modifierMappingService;

    public ProductService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IModifierRepository modifierRepository,
        IProductValidationService productValidationService,
        IProductMappingService productMappingService,
        IModifierMappingService modifierMappingService
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _modifierRepository = modifierRepository;
        _productValidationService = productValidationService;
        _productMappingService = productMappingService;
        _modifierMappingService = modifierMappingService;
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
            Modifiers = [],
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
        var products = await _productRepository.GetPagedWithTaxes(paginationFilter);
        return _productMappingService.MapToPagedProductDTO(products, paginationFilter);
    }

    public async Task DeleteProduct(int productId)
    {
        await _productRepository.Delete(productId);
    }

    public async Task SetProductModifiers(int productId, SetModifiersForProductDTO setModifiersForProductDTO)
    {
        var product = await _productRepository.GetWithModifiers(productId);
        var newModifierIds = setModifiersForProductDTO.ModifierIds.Distinct().ToList();
        var existingModifierIds = product.Modifiers.Select(m => m.Id).ToList();

        var modifiersToDeleteIds = existingModifierIds.Except(newModifierIds);
        product.Modifiers.RemoveAll(m => modifiersToDeleteIds.Contains(m.Id));

        var modifiersToAddIds = newModifierIds.Except(existingModifierIds);
        var modifiersToAdd = await _modifierRepository.GetMany(modifiersToAddIds);
        product.Modifiers.AddRange(modifiersToAdd);

        await _unitOfWork.SaveChanges();
    }

    public async Task<PagedResponseDTO<ModifierDTO>> GetProductModifiers(
        int productId,
        PaginationFilterDTO paginationFilterDTO
    )
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var modifierFilter = new ModifierFilter { CompatibleWithProductById = productId };
        var modifiers = await _modifierRepository.GetWithFilter(paginationFilter, modifierFilter);
        return _modifierMappingService.MapToPagedModifierDTO(modifiers, paginationFilter);
    }
}
