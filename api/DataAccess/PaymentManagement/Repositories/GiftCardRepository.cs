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

    public async Task<bool> IsCodeUsed(string code)
    {
        return await DbSet.AnyAsync(g => g.Code == code.ToUpper());
    }

    public async Task<GiftCard> GetByCode(string code)
    {
        var giftCard = await DbSet.FirstOrDefaultAsync(g => g.Code == code.ToUpper());
        if (giftCard is null)
        {
            throw new EntityNotFoundException(new GiftCardWithCodeNotFoundErrorMessage(code.ToUpper()));
        }

        return giftCard;
    }

    public async Task<List<GiftCard>> GetWithPagination(PaginationFilter paginationFilter)
    {
        var query = DbSet.OrderBy(g => g.CreatedAt).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new GiftCardNotFoundErrorMessage(id);
    }
}
