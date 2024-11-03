using PointOfSale.BusinessLogic.Order.DTOs;
using PointOfSale.BusinessLogic.Order.Interfaces;
using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.Order.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly ITaxRepository _taxRepository;

    public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ITaxRepository taxRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _taxRepository = taxRepository;
    }

    public async Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO)
    {
        // TODO: validation
        
        var taxes = await _taxRepository.GetMany(createProductDTO.TaxIds);
        
        var product = new Product
        {
            Name = createProductDTO.Name,
            Price = createProductDTO.Price,
            Stock = createProductDTO.Stock,
            Taxes = taxes
        };
        
        _productRepository.Add(product);
        await _unitOfWork.SaveChanges();

        return ProductDTO.Create(product);
    }
}