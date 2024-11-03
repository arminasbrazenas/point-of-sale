using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            AuditChangedEntities(eventData.Context);
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void AuditChangedEntities(DbContext dbContext)
    {
        var addedEntityEntries = dbContext.ChangeTracker.Entries<IAuditable>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .ToList();

        foreach (var entityEntry in addedEntityEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            }
            
            entityEntry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }
}