using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class ReservationRepository : RepositoryBase<Reservation, int>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext) 
        : base(dbContext) { }

    
    public async Task<List<Reservation>> GetPaged(PaginationFilter paginationFilter)
    {
        var query = DbSet.OrderBy(s => s.CreatedAt).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }
    
    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ReservationNotFoundErrorMessage(id);
    }
}

