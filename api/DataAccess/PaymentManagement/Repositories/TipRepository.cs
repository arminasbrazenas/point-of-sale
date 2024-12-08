using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Repositories;

public class TipRepository : RepositoryBase<Tip, int>, ITipRepository
{
    public TipRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<Tip>> GetOrderTips(int orderId)
    {
        return await DbSet.Where(t => t.OrderId == orderId).OrderBy(t => t.CreatedAt).ToListAsync();
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new TipNotFoundErrorMessage(id);
    }
}
