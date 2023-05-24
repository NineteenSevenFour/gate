using NineteenSevenFour.Gatehub.Domain.Entities;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces;

public interface IDefaultRepository<TEntity> where TEntity : BaseEntity
{
  Task Add(TEntity entity);

  Task AddRange(IEnumerable<TEntity> entities);

  Task Update(TEntity entity);

  Task<TEntity?> GetById(int id);

  IQueryable<TEntity> GetAll();

  IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

  Task Remove(TEntity entity);

  Task RemoveRange(IEnumerable<TEntity> entities);
}

