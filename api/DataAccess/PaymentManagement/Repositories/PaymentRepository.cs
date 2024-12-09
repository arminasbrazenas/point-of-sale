using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.DataAccess.PaymentManagement.Repositories;

public class PaymentRepository : RepositoryBase<Payment, int>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<Payment>> GetOrderPayments(int orderId)
    {
        return await DbSet.Where(p => p.OrderId == orderId).ToListAsync();
    }

    public async Task<List<OnlinePayment>> GetPendingOnlinePayments()
    {
        return await DbSet
            .Where(p => p.Method == PaymentMethod.Online && p.Status == PaymentStatus.Pending)
            .Select(p => (OnlinePayment)p)
            .ToListAsync();
    }

    public async Task<List<OnlinePayment>> GetPendingOnlinePaymentsOlderThan(TimeSpan olderThan)
    {
        var dateLimit = DateTime.UtcNow.Subtract(olderThan);
        return await DbSet
            .Where(p =>
                p.Method == PaymentMethod.Online && p.Status == PaymentStatus.Pending && p.CreatedAt < dateLimit
            )
            .Select(p => (OnlinePayment)p)
            .ToListAsync();
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new PaymentNotFoundErrorMessage(id);
    }
}
