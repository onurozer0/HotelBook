using HotelBook.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HotelBook.Data.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, BaseEntity>> Behaviors = new()
    {
        {EntityState.Added,AddBehavior},
        {EntityState.Modified,UpdateBehavior},
    };
    private static void AddBehavior(DbContext context, BaseEntity entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.IsActive = true;
        context.Entry(entity).Property(x => x.UpdatedDate).IsModified = false;
    }
    private static void UpdateBehavior(DbContext context, BaseEntity entity)
    {
        entity.UpdatedDate = DateTime.Now;
        context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        foreach (var entry in eventData.Context.ChangeTracker.Entries().ToList())
        {
            if (entry.Entity is not BaseEntity entity) continue;

            Behaviors[entry.State](eventData.Context, entity);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}