using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
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

    public async Task<List<CardPayment>> GetPendingCardPayments()
    {
        return await DbSet
            .Where(p => p.Method == PaymentMethod.Card && p.Status == PaymentStatus.Pending)
            .Select(p => (CardPayment)p)
            .ToListAsync();
    }

    public async Task<List<CardPayment>> GetPendingCardPaymentsOlderThan(TimeSpan olderThan)
    {
        var dateLimit = DateTime.UtcNow.Subtract(olderThan);
        return await DbSet
            .Where(p => p.Method == PaymentMethod.Card && p.Status == PaymentStatus.Pending && p.CreatedAt < dateLimit)
            .Select(p => (CardPayment)p)
            .ToListAsync();
    }

    public async Task<CardPayment> GetCardPaymentByExternalId(string externalId)
    {
        var payment = await DbSet.FirstOrDefaultAsync(p =>
            p.Method == PaymentMethod.Card && ((CardPayment)p).ExternalId == externalId
        );

        if (payment is null)
        {
            throw new EntityNotFoundException(new CardPaymentNotFoundErrorMessage(externalId));
        }

        return (CardPayment)payment;
    }

    public async Task<List<CardPayment>> GetInitiatedCardRefunds()
    {
        return await DbSet
            .Where(p => p.Method == PaymentMethod.Card && p.Status == PaymentStatus.RefundInitiated)
            .Select(p => (CardPayment)p)
            .ToListAsync();
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new PaymentNotFoundErrorMessage(id);
    }
}
