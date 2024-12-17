using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.BusinessLogic.PaymentManagement.Services;

public class GiftCardMappingService : IGiftCardMappingService
{
    public GiftCardDTO MapToGiftCardDTO(GiftCard giftCard)
    {
        return new GiftCardDTO
        {
            Id = giftCard.Id,
            Code = giftCard.Code,
            Amount = giftCard.Amount,
            ExpiresAt = giftCard.ExpiresAt,
            UsedAt = giftCard.UsedAt,
            BusinessId = giftCard.BusinessId,
        };
    }

    public PagedResponseDTO<GiftCardDTO> MapToPagedGiftCardDTO(
        List<GiftCard> giftCards,
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<GiftCardDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Items = giftCards.Select(MapToGiftCardDTO).ToList(),
        };
    }
}
