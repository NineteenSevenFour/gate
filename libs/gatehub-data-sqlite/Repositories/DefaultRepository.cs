using Microsoft.EntityFrameworkCore;
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
      var result = await context.Set<TEntity>().AddAsync(entity);
      await context.SaveChangesAsync();
      return result.Entity;
    }

    /// <inheritdoc/>
    public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
    {
      await context.Set<TEntity>().AddRangeAsync(entities);
      return await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
      var result = context.Set<TEntity>().Update(entity);
      await context.SaveChangesAsync();
      return result.Entity;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
      return await context.Set<TEntity>().FindAsync(id);
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> GetAll()
    {
      return context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
      var entities = context.Set<TEntity>().AsNoTracking();
      if (expression?.Body != null)
      {
        entities = entities.Where(expression);
      }
      return entities;
    }

    /// <inheritdoc/>
    public virtual async Task<int> RemoveAsync(int id)
    {
      var entity = await this.Find((e) => e.Id == id)
                             .AsNoTracking()
                             .FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException($"No entity with Id {id} could be found.");
      context.Set<TEntity>().Remove(entity);
      return await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
      context.Set<TEntity>().RemoveRange(entities);
      return await context.SaveChangesAsync();
    }
  }
}
