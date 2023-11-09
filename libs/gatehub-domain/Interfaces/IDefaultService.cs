using NineteenSevenFour.Gatehub.Domain.Models;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces;

/// <summary>
/// Default service interface
/// </summary>
/// <typeparam name="TModel">The model type used by the service</typeparam>
public interface IDefaultService<TModel>
  where TModel : class, new()
{
  /// <summary>
  /// Add a model asyncronously
  /// </summary>
  /// <param name="model">The model to add</param>
  /// <returns>The added model with its unique identifier</returns>
  Task<TModel> AddAsync(TModel model);

  /// <summary>
  /// Add a set of model asyncronously
  /// </summary>
  /// <param name="models">The set of model to add</param>
  /// <returns>A count of added model</returns>
  Task<int> AddRangeAsync(IEnumerable<TModel> models);

  /// <summary>
  /// Update a model asyncronously
  /// </summary>
  /// <param name="model">the model to update</param>
  /// <returns>?The updated model</returns>
  Task<TModel> UpdateAsync(TModel model);

  /// <summary>
  /// Get a model asyncronously from its Id
  /// </summary>
  /// <param name="id">The unique identifier of the model to get</param>
  /// <returns>A model or null if not found</returns>
  Task<TModel?> GetByIdAsync(int id);

  /// <summary>
  /// Get all entity asyncronously from the repository
  /// </summary>
  /// <returns>A queryable list of model</returns>
  IQueryable<TModel> GetAll();

  /// <summary>
  /// Get an entity asyncronously from the repository using open query
  /// </summary>
  /// <param name="expression">The expression representing the filter query</param>
  /// <returns>A queryable list of model</returns>
  IQueryable<TModel> Find(Expression<Func<TModel, bool>> expression);

  /// <summary>
  /// Remove an entity asyncronously from the repository
  /// </summary>
  /// <param name="id">The unique identifier of the model to remove</param>
  /// <returns>1 if the model was removed, otherwise 0</returns>
  Task<int> RemoveAsync(int id);

  /// <summary>
  /// Remove a set of model asyncronously from the repository
  /// </summary>
  /// <param name="models">The set of model to remove</param>
  /// <returns>A count of removed model</returns>
  Task<int> RemoveRangeAsync(IEnumerable<TModel> models);
}
