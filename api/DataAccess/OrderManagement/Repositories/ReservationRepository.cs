using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class ReservationRepository : RepositoryBase<Reservation, int>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Reservation> GetWithRelatedData(int reservationId)
    {
        var reservation = await DbSet
            .Include(r => r.Employee)
            .Include(r => r.Service)
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation is null)
        {
            throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(reservationId));
        }

        return reservation;
    }

    public async Task<List<Reservation>> GetPaged(PaginationFilter paginationFilter, int businessId)
    {
        var query = DbSet
            .Where(r => r.BusinessId == businessId)
            .Include(r => r.Employee)
            .OrderBy(s => s.Date.Start)
            .AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    public override async Task<int> GetTotalCount(int? businessId = null)
    {
        if (businessId.HasValue)
        {
            return await DbSet.Where(o => o.BusinessId == businessId).CountAsync();
        }

        return await base.GetTotalCount(businessId);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ReservationNotFoundErrorMessage(id);
    }
}
