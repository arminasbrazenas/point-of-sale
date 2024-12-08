using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IGiftCardService
{
    Task<GiftCardDTO> CreateGiftCard(CreateGiftCardDTO createGiftCardDTO);
    Task<GiftCardDTO> GetGiftCard(int giftCardId);
    Task<PagedResponseDTO<GiftCardDTO>> GetGiftCards(PaginationFilterDTO paginationFilterDTO);
    Task<GiftCardDTO> UpdateGiftCard(int giftCardId, UpdateGiftCardDTO updateGiftCardDTO);
    Task DeleteGiftCard(int giftCardId);
}
