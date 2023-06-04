using NineteenSevenFour.Gatehub.Domain.Entities;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces;

public interface IDefaultRepository<TEntity> where TEntity : BaseEntity
{
  Task<TEntity> AddAsync(TEntity entity);

  Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

  Task<TEntity> UpdateAsync(TEntity entity);

  Task<TEntity?> GetByIdAsync(int id);

  IQueryable<TEntity> GetAll();

  IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

  Task<int> RemoveAsync(int id);

  Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);
}
