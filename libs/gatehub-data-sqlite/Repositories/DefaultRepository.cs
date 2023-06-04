using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using NineteenSevenFour.Gatehub.Data.Sqlite.Context;
using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Repositories
{
  public class DefaultRepository<TEntity> : IDefaultRepository<TEntity> where TEntity : BaseEntity
  {
    protected readonly SqliteDbContext context;
    protected readonly ILogger<DefaultRepository<TEntity>> logger;

    public DefaultRepository(ILoggerFactory loggerFactory, SqliteDbContext context)
    {
      this.context = context;
      logger = loggerFactory.CreateLogger<DefaultRepository<TEntity>>();
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
      if (entity == null) throw new ArgumentNullException(nameof(entity));

      var result = await context.AddAsync(entity);
      if ((await context.SaveChangesAsync()) == 1)
      {
        return result.Entity;
      }
      throw new DbUpdateException($"Could not add the {nameof(TEntity)}", new List<EntityEntry<TEntity>>() { result });
    }

    /// <inheritdoc/>
    public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
    {
      if (entities == null) throw new ArgumentNullException(nameof(entities));

      await context.AddRangeAsync(entities);
      var count = await context.SaveChangesAsync();
      if (count == entities.Count())
      {
        return count;
      }
      throw new DbUpdateException($"Could not add the list of {nameof(TEntity)}");
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
      if (entity == null) throw new ArgumentNullException(nameof(entity));

      var result = context.Update(entity);
      if ((await context.SaveChangesAsync()) == 1)
      {
        return result.Entity;
      }
      throw new DbUpdateException($"Could not update the {nameof(TEntity)}", new List<EntityEntry<TEntity>>() { result });
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
      return await context.FindAsync<TEntity>(id);
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> GetAll()
    {
      return context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
      if (expression?.Body == null) throw new ArgumentNullException(nameof(expression));

      return context.Set<TEntity>().Where(expression);
    }

    /// <inheritdoc/>
    public virtual async Task<int> RemoveAsync(int id)
    {
      var entity = await context.FindAsync<TEntity>(id) ?? throw new ArgumentOutOfRangeException($"No entity with Id {id} could be found.");
      var resultEntries = context.Remove<TEntity>(entity);
      var results = await context.SaveChangesAsync();
      if (results == 1)
      {
        return results;
      }
      throw new DbUpdateException($"Could not remove the {nameof(TEntity)}", new List<EntityEntry<TEntity>>() { resultEntries });
    }

    /// <inheritdoc/>
    public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
      if (entities == null) throw new ArgumentNullException(nameof(entities));

      context.RemoveRange(entities);
      var results = await context.SaveChangesAsync();
      if (results == entities.Count())
      {
        return results;
      }
      throw new DbUpdateException($"Could not remove the list of {nameof(TEntity)}");
    }
  }
}
