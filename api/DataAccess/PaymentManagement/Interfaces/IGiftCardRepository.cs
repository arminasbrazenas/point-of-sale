using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Interfaces;

public interface IGiftCardRepository : IRepositoryBase<GiftCard, int>
{
    Task<bool> IsCodeUsed(string code);
    Task<GiftCard> GetByCode(string code);
    Task<List<GiftCard>> GetWithPagination(int businessId, PaginationFilter paginationFilter);
    Task<int> GetTotalCount(int businessId);
}
