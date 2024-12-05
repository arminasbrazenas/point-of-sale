using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
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

    public DiscountService(
        IProductRepository productRepository,
        IDiscountRepository discountRepository,
        IUnitOfWork unitOfWork,
        IDiscountMappingService discountMappingService
    )
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
            ValidUntil = createDiscountDTO.ValidUntil,
        };

        _discountRepository.Add(discount);
        await _unitOfWork.SaveChanges();

        return _discountMappingService.MapToDiscountDTO(discount);
    }

    public async Task<DiscountDTO> GetDiscount(int discountId)
    {
        var discount = await _discountRepository.GetWithProducts(discountId);
        return _discountMappingService.MapToDiscountDTO(discount);
    }

    public async Task<PagedResponseDTO<DiscountDTO>> GetDiscounts(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var discounts = await _discountRepository.GetPagedWithProducts(paginationFilter);
        var totalCount = await _discountRepository.GetTotalCount();
        return _discountMappingService.MapToPagedDiscountDTO(discounts, paginationFilter, totalCount);
    }

    public async Task<DiscountDTO> UpdateDiscount(int discountId, UpdateDiscountDTO updateDiscountDTO)
    {
        var discount = await _discountRepository.GetWithProducts(discountId);

        if (updateDiscountDTO.Amount.HasValue)
        {
            discount.Amount = updateDiscountDTO.Amount.Value;
        }

        if (updateDiscountDTO.PricingStrategy.HasValue)
        {
            discount.PricingStrategy = updateDiscountDTO.PricingStrategy.Value;
        }

        if (updateDiscountDTO.ValidUntil.HasValue)
        {
            discount.ValidUntil = updateDiscountDTO.ValidUntil.Value;
        }

        if (updateDiscountDTO.AppliesToProductIds is not null)
        {
            var products = await _productRepository.GetMany(updateDiscountDTO.AppliesToProductIds);
            discount.AppliesTo = products;
        }

        await _unitOfWork.SaveChanges();
        return _discountMappingService.MapToDiscountDTO(discount);
    }

    public async Task DeleteDiscount(int discountId)
    {
        await _discountRepository.Delete(discountId);
    }
}
