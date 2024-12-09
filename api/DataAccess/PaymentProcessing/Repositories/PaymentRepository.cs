using PointOfSale.DataAccess.PaymentProcessing.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.PaymentProcessing.Repositories;

public class PaymentRepository : RepositoryBase<OrderPayment, int>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext dbContext)
    : base(dbContext) { }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        throw new NotImplementedException();
    }
}