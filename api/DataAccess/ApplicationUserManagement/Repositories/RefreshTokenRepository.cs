using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Repositories;

public class RefreshTokenRepository : RepositoryBase<RefreshToken, int>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new RefreshTokenNotFoundErrorMessage(id);
    }

    public async Task<RefreshToken?> GetRefreshTokenByHash(string hashedToken)
    {
        return await DbSet.Include(t => t.ApplicationUser).FirstOrDefaultAsync(t => t.RefreshTokenHash == hashedToken);
    }
}
