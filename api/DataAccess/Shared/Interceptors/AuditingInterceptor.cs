using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.Models.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;

    public AuditingInterceptor(ICurrentApplicationUserAccessor currentApplicationUserAccessor)
    {
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is not null)
        {
            AuditChangedEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AuditChangedEntities(DbContext dbContext)
    {
        var addedEntityEntries = dbContext
            .ChangeTracker.Entries<IAuditable>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .ToList();

        foreach (var entityEntry in addedEntityEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            if(_currentApplicationUserAccessor.GetApplicationUserIdOrDefault() is { } createdApplicationUserId)
            {
                entityEntry.Entity.CreatedById = createdApplicationUserId;
            }
            }

            entityEntry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
            if (_currentApplicationUserAccessor.GetApplicationUserIdOrDefault() is { } modifiedApplicationUserId)
            {
                entityEntry.Entity.ModifiedById = modifiedApplicationUserId;
            }
        }
    }
}
