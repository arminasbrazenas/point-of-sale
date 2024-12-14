using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
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

    public GiftCardService(
        IGiftCardRepository giftCardRepository,
        IUnitOfWork unitOfWork,
        IGiftCardMappingService giftCardMappingService
    )
    {
        _giftCardRepository = giftCardRepository;
        _unitOfWork = unitOfWork;
        _giftCardMappingService = giftCardMappingService;
    }

    public async Task<GiftCardDTO> CreateGiftCard(CreateGiftCardDTO createGiftCardDTO)
    {
        await ValidateGiftCardCodeIsUnique(createGiftCardDTO.Code);

        var giftCard = new GiftCard
        {
            Code = createGiftCardDTO.Code.ToUpper(),
            Amount = createGiftCardDTO.Amount.ToRoundedPrice(),
            ExpiresAt = createGiftCardDTO.ExpiresAt,
            UsedAt = null,
        };

        _giftCardRepository.Add(giftCard);
        await _unitOfWork.SaveChanges();

        return _giftCardMappingService.MapToGiftCardDTO(giftCard);
    }

    public async Task<GiftCardDTO> GetGiftCard(int giftCardId)
    {
        var giftCard = await _giftCardRepository.Get(giftCardId);
        return _giftCardMappingService.MapToGiftCardDTO(giftCard);
    }

    public async Task<PagedResponseDTO<GiftCardDTO>> GetGiftCards(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var giftCards = await _giftCardRepository.GetWithPagination(paginationFilter);
        var totalCount = await _giftCardRepository.GetTotalCount();
        return _giftCardMappingService.MapToPagedGiftCardDTO(giftCards, paginationFilter, totalCount);
    }

    public async Task<GiftCardDTO> UpdateGiftCard(int giftCardId, UpdateGiftCardDTO updateGiftCardDTO)
    {
        var giftCard = await _giftCardRepository.Get(giftCardId);
        if (giftCard.UsedAt is not null)
        {
            throw new ValidationException(new UsedGiftCardCannotBeModifiedErrorMessage());
        }

        if (updateGiftCardDTO.Code is not null)
        {
            await ValidateGiftCardCodeIsUnique(updateGiftCardDTO.Code);
            giftCard.Code = updateGiftCardDTO.Code.ToUpper();
        }

        if (updateGiftCardDTO.Amount.HasValue)
        {
            giftCard.Amount = updateGiftCardDTO.Amount.Value.ToRoundedPrice();
        }

        if (updateGiftCardDTO.ExpiresAt.HasValue)
        {
            giftCard.ExpiresAt = updateGiftCardDTO.ExpiresAt.Value;
        }

        await _unitOfWork.SaveChanges();

        return _giftCardMappingService.MapToGiftCardDTO(giftCard);
    }

    public async Task DeleteGiftCard(int giftCardId)
    {
        await _giftCardRepository.Delete(giftCardId);
    }

    public async Task<GiftCardDTO> GetUsableGiftCardByCode(string code)
    {
        var giftCard = await _giftCardRepository.GetByCode(code);
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
        giftCard.UsedAt = DateTimeOffset.UtcNow;
        await _unitOfWork.SaveChanges();
    }

    private async Task ValidateGiftCardCodeIsUnique(string code)
    {
        var isCodeUsed = await _giftCardRepository.IsCodeUsed(code);
        if (isCodeUsed)
        {
            throw new ValidationException(new DuplicateGiftCardCodeErrorMessage(code));
        }
    }
}
