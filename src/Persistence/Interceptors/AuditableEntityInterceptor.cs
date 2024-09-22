using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors;
public class AuditableEntityInterceptor(IUser user, TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var utcNow = timeProvider.GetUtcNow();
        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>().Where(entry => entry.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = user.Id;
                entry.Entity.Created = utcNow;
            }

            entry.Entity.LastModifiedBy = user.Id;
            entry.Entity.LastModified = utcNow;
        }
    }
}
