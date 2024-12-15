using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class DiscountService : IDiscountService
{
    private readonly IProductRepository _productRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscountMappingService _discountMappingService;
    private readonly IOrderManagementAuthorizationService _orderManagementAuthorizationService;

    public DiscountService(
        IProductRepository productRepository,
        IDiscountRepository discountRepository,
        IUnitOfWork unitOfWork,
        IDiscountMappingService discountMappingService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService
    )
    {
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
        _discountMappingService = discountMappingService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
    }

    public async Task<DiscountDTO> CreateDiscount(CreateDiscountDTO createDiscountDTO)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(createDiscountDTO.BusinessId);
        var products = await _productRepository.GetMany(createDiscountDTO.AppliesToProductIds);
        switch (createDiscountDTO)
        {
            case { Target: DiscountTarget.Product, AppliesToProductIds: null }:
                throw new ValidationException(new EntitledDiscountMustHaveProductsErrorMessage());
            case { Target: DiscountTarget.Order, AppliesToProductIds: not null }:
                throw new ValidationException(new EverythingDiscountCannotBeAppliedToProductsErrorMessage());
        }

        if (createDiscountDTO.AppliesToProductIds is not null)
        {
            products = await _productRepository.GetMany(createDiscountDTO.AppliesToProductIds);
        }

        var discount = new Discount
        {
            Amount = createDiscountDTO.Amount,
            PricingStrategy = createDiscountDTO.PricingStrategy,
            AppliesTo = products,
            ValidUntil = createDiscountDTO.ValidUntil,
            BusinessId = createDiscountDTO.BusinessId,
            Target = createDiscountDTO.Target,
        };

        _discountRepository.Add(discount);
        await _unitOfWork.SaveChanges();

        return _discountMappingService.MapToDiscountDTO(discount);
    }

    public async Task<DiscountDTO> GetDiscount(int discountId)
    {
        var discount = await _discountRepository.GetWithProducts(discountId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(discount.BusinessId);

        return _discountMappingService.MapToDiscountDTO(discount);
    }

    public async Task<PagedResponseDTO<DiscountDTO>> GetDiscounts(
        PaginationFilterDTO paginationFilterDTO,
        int businessId
    )
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var discounts = await _discountRepository.GetPagedWithProducts(paginationFilter, businessId);
        var totalCount = await _discountRepository.GetTotalCount();
        return _discountMappingService.MapToPagedDiscountDTO(discounts, paginationFilter, totalCount);
    }

    public async Task<DiscountDTO> UpdateDiscount(int discountId, UpdateDiscountDTO updateDiscountDTO)
    {
        var discount = await _discountRepository.GetWithProducts(discountId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(discount.BusinessId);

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
            if (discount.Target == DiscountTarget.Order)
            {
                throw new ValidationException(new EverythingDiscountCannotBeAppliedToProductsErrorMessage());
            }

            var products = await _productRepository.GetMany(updateDiscountDTO.AppliesToProductIds);
            discount.AppliesTo = products;
        }

        await _unitOfWork.SaveChanges();
        return _discountMappingService.MapToDiscountDTO(discount);
    }

    public async Task DeleteDiscount(int discountId)
    {
        var discount = await _discountRepository.Get(discountId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(discount.BusinessId);

        await _discountRepository.Delete(discountId);
    }
}
