using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.BusinessLogic.PaymentManagement.Services;

public class GiftCardService : IGiftCardService
{
    private readonly IGiftCardRepository _giftCardRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGiftCardMappingService _giftCardMappingService;
    private readonly IOrderManagementAuthorizationService _orderManagementAuthorizationService;
    private readonly IGiftCardValidationService _giftCardValidationService;

    public GiftCardService(
        IGiftCardRepository giftCardRepository,
        IUnitOfWork unitOfWork,
        IGiftCardMappingService giftCardMappingService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService,
        IGiftCardValidationService giftCardValidationService
    )
    {
        _giftCardRepository = giftCardRepository;
        _unitOfWork = unitOfWork;
        _giftCardMappingService = giftCardMappingService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
        _giftCardValidationService = giftCardValidationService;
    }

    public async Task<GiftCardDTO> CreateGiftCard(CreateGiftCardDTO createGiftCardDTO)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(createGiftCardDTO.BusinessId);

        var code = await _giftCardValidationService.ValidateCode(
            createGiftCardDTO.Code.ToUpper(),
            createGiftCardDTO.BusinessId
        );
        var amount = _giftCardValidationService.ValidateAmount(createGiftCardDTO.Amount.ToRoundedPrice());
        var expiresAt = _giftCardValidationService.ValidateExpiration(createGiftCardDTO.ExpiresAt);

        var giftCard = new GiftCard
        {
            Code = code,
            Amount = amount,
            ExpiresAt = expiresAt,
            UsedAt = null,
            BusinessId = createGiftCardDTO.BusinessId,
        };

        _giftCardRepository.Add(giftCard);
        await _unitOfWork.SaveChanges();

        return _giftCardMappingService.MapToGiftCardDTO(giftCard);
    }

    public async Task<GiftCardDTO> GetGiftCard(int giftCardId)
    {
        var giftCard = await _giftCardRepository.Get(giftCardId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(giftCard.BusinessId);

        return _giftCardMappingService.MapToGiftCardDTO(giftCard);
    }

    public async Task<PagedResponseDTO<GiftCardDTO>> GetGiftCards(
        int businessId,
        PaginationFilterDTO paginationFilterDTO
    )
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(businessId);

        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var giftCards = await _giftCardRepository.GetWithPagination(businessId, paginationFilter);
        var totalCount = await _giftCardRepository.GetTotalCount(businessId);
        return _giftCardMappingService.MapToPagedGiftCardDTO(giftCards, paginationFilter, totalCount);
    }

    public async Task<GiftCardDTO> UpdateGiftCard(int giftCardId, UpdateGiftCardDTO updateGiftCardDTO)
    {
        var giftCard = await _giftCardRepository.Get(giftCardId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(giftCard.BusinessId);

        if (giftCard.UsedAt is not null)
        {
            throw new ValidationException(new UsedGiftCardCannotBeModifiedErrorMessage());
        }

        if (updateGiftCardDTO.Code is not null)
        {
            giftCard.Code = await _giftCardValidationService.ValidateCode(
                updateGiftCardDTO.Code.ToUpper(),
                giftCard.BusinessId
            );
        }

        if (updateGiftCardDTO.Amount.HasValue)
        {
            giftCard.Amount = _giftCardValidationService.ValidateAmount(
                updateGiftCardDTO.Amount.Value.ToRoundedPrice()
            );
        }

        if (updateGiftCardDTO.ExpiresAt.HasValue)
        {
            giftCard.ExpiresAt = _giftCardValidationService.ValidateExpiration(updateGiftCardDTO.ExpiresAt.Value);
        }

        await _unitOfWork.SaveChanges();

        return _giftCardMappingService.MapToGiftCardDTO(giftCard);
    }

    public async Task DeleteGiftCard(int giftCardId)
    {
        var giftCard = await _giftCardRepository.Get(giftCardId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(giftCard.BusinessId);

        await _giftCardRepository.Delete(giftCardId);
    }

    public async Task<GiftCardDTO> GetUsableGiftCardByCode(string code, int businessId)
    {
        var giftCard = await _giftCardRepository.GetByCode(code.ToUpper(), businessId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(giftCard.BusinessId);

        if (giftCard.UsedAt is not null)
        {
            throw new ValidationException(new GiftCardIsAlreadyUsedErrorMessage());
        }

        if (giftCard.ExpiresAt < DateTimeOffset.UtcNow)
        {
            throw new ValidationException(new GiftCardIsExpiredErrorMessage());
        }

        return _giftCardMappingService.MapToGiftCardDTO(giftCard);
    }

    public async Task MarkGiftCardAsUsed(int giftCardId)
    {
        var giftCard = await _giftCardRepository.Get(giftCardId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(giftCard.BusinessId);

        giftCard.UsedAt = DateTimeOffset.UtcNow;
        await _unitOfWork.SaveChanges();
    }
}
