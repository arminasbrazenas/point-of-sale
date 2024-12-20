using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Repositories;

public class GiftCardRepository : RepositoryBase<GiftCard, int>, IGiftCardRepository
{
    public GiftCardRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<bool> IsCodeUsed(string code, int businessId)
    {
        return await DbSet.AnyAsync(g => g.Code == code && g.BusinessId == businessId);
    }

    public async Task<GiftCard> GetByCode(string code, int businessId)
    {
        var giftCard = await DbSet.FirstOrDefaultAsync(g => g.Code == code && g.BusinessId == businessId);
        if (giftCard is null)
        {
            throw new EntityNotFoundException(new GiftCardWithCodeNotFoundErrorMessage(code));
        }

        return giftCard;
    }

    public async Task<List<GiftCard>> GetWithPagination(int businessId, PaginationFilter paginationFilter)
    {
        var query = DbSet.Where(g => g.BusinessId == businessId).OrderBy(g => g.CreatedAt).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new GiftCardNotFoundErrorMessage(id);
    }

    public async Task<int> GetTotalCount(int businessId)
    {
        return await DbSet.Where(d => d.BusinessId == businessId).CountAsync();
    }
}
