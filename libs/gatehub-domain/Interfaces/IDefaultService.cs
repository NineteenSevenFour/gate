using NineteenSevenFour.Gatehub.Domain.Models;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces;

public interface IDefaultService<TModel>
  where TModel : BaseModel
{
  Task Add(TModel entity);

  Task AddRange(IEnumerable<TModel> entities);

  Task Update(TModel entity);

  Task<TModel> GetById(int id);

  IQueryable<TModel> GetAll();

  IQueryable<TModel> Find(Expression<Func<TModel, bool>> expression);

  Task Remove(TModel entity);

  Task RemoveRange(IEnumerable<TModel> entities);
}
