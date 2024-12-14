using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IGiftCardMappingService
{
    GiftCardDTO MapToGiftCardDTO(GiftCard giftCard);
    PagedResponseDTO<GiftCardDTO> MapToPagedGiftCardDTO(
        List<GiftCard> giftCards,
        PaginationFilter paginationFilter,
        int totalCount
    );
}
