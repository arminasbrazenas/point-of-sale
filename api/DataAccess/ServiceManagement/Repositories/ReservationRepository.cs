using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Repositories;

public class ReservationRepository : RepositoryBase<Reservation, int>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext) 
        : base(dbContext) { }
   
   public List<int> GetServiceResourceIdsByTime(DateTime start, DateTime end)
   {
       return DbSet.Where(r => r.DateStart < end && r.DateEnd > start)
           .Select(r => r.ServiceResourceId)
           .ToList();
   }
    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        throw new NotImplementedException();
    }
}