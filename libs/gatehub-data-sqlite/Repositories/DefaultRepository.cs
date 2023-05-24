using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NineteenSevenFour.Gatehub.Data.sqlite.Context;
using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Data.sqlite.Repositories
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
    public virtual async Task Add(TEntity entity)
    {
      await context.Set<TEntity>().AddAsync(entity);
      context.SaveChanges();
    }

    /// <inheritdoc/>
    public virtual Task Update(TEntity entity)
    {
      context.Set<TEntity>().Update(entity);
      context.SaveChanges();
      return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task AddRange(IEnumerable<TEntity> entities)
    {
      await context.Set<TEntity>().AddRangeAsync(entities);
      context.SaveChanges();
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
    public virtual IQueryable<TEntity> GetAll()
    {
      return context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetById(int id)
    {
      return await context.Set<TEntity>().FindAsync(id);
    }

    /// <inheritdoc/>
    public virtual Task Remove(TEntity entity)
    {
      context.Set<TEntity>().Remove(entity);
      context.SaveChanges();
      return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task RemoveRange(IEnumerable<TEntity> entities)
    {
      context.Set<TEntity>().RemoveRange(entities);
      return Task.CompletedTask;
    }
  }
}
