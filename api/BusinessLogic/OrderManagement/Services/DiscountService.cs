using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class DiscountService : IDiscountService
{
    private readonly IProductRepository _productRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscountMappingService _discountMappingService;

    public DiscountService(IProductRepository productRepository, IDiscountRepository discountRepository, IUnitOfWork unitOfWork, IDiscountMappingService discountMappingService)
    {
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
        _discountMappingService = discountMappingService;
    }

    public async Task<DiscountDTO> CreateDiscount(CreateDiscountDTO createDiscountDTO)
    {
        var products = await _productRepository.GetMany(createDiscountDTO.AppliesToProductIds);
        var discount = new Discount
        {
            Amount = createDiscountDTO.Amount,
            PricingStrategy = createDiscountDTO.PricingStrategy,
            AppliesTo = products,
            ValidUntil = createDiscountDTO.ValidUntil
        };
        
        _discountRepository.Add(discount);
        await _unitOfWork.SaveChanges();

        return _discountMappingService.MapToDiscountDTO(discount);
    }
}