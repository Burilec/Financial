using Burile.Financial.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Burile.Financial.Infrastructure.Interceptors;

// ReSharper disable once UnusedType.Global
public sealed class CreatableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventData);

        var creatableEntries = eventData.Context?.ChangeTracker.Entries<ICreatableEntity>()
                            ?? Enumerable.Empty<EntityEntry<ICreatableEntity>>();

        foreach (var creatableEntry in creatableEntries)
        {
            if (creatableEntry.State is EntityState.Added)
            {
                creatableEntry.Entity.SetCreationDateTime(DateTime.UtcNow);
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken)
                         .ConfigureAwait(false);
    }
}