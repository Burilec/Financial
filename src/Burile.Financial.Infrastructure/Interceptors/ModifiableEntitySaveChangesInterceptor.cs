using Burile.Financial.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Burile.Financial.Infrastructure.Interceptors;

// ReSharper disable once UnusedType.Global
public sealed class ModifiableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventData);

        var modifiableEntries = eventData.Context?.ChangeTracker.Entries<IModifiableEntity>()
                             ?? Enumerable.Empty<EntityEntry<IModifiableEntity>>();

        foreach (var modifiableEntry in modifiableEntries)
        {
            if (modifiableEntry.State is EntityState.Detached or EntityState.Modified)
            {
                modifiableEntry.Entity.SetModificationDateTime(DateTime.UtcNow);
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken)
                         .ConfigureAwait(false);
    }
}