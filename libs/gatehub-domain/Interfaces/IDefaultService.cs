using NineteenSevenFour.Gatehub.Domain.Models;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces;

public interface IDefaultService<TModel>
  where TModel : BaseModel
{
  Task<TModel> AddAsync(TModel model);

  Task<int> AddRangeAsync(IEnumerable<TModel> models);

  Task<TModel> UpdateAsync(TModel model);

  Task<TModel?> GetByIdAsync(int id);

  IQueryable<TModel> GetAll();

  IQueryable<TModel> Find(Expression<Func<TModel, bool>> expression);

  Task<int> RemoveAsync(int id);

  Task<int> RemoveRangeAsync(IEnumerable<TModel> models);
}
