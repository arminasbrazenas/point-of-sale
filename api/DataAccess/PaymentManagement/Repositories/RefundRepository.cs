using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Repositories
{
    public class RefundRepository : RepositoryBase<PaymentRefund, int>, IRefundRepository
    {
        public RefundRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<List<PaymentRefund>> GetRefundsByPaymentIntentIdAsync(string paymentIntentId)
        {
            return await DbSet.Where(r => r.PaymentIntentId == paymentIntentId).ToListAsync();
        }

        protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
        {
            return new RefundNotFoundErrorMessage(id);
        }
    }
}
