using NineteenSevenFour.Gatehub.Domain.Entities;

using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces;

/// <summary>
/// Default repository interface
/// </summary>
/// <typeparam name="TEntity">Entity type used by the repository</typeparam>
public interface IDefaultRepository<TEntity> where TEntity : BaseEntity
{
  /// <summary>
  /// Add an entity asyncronously to the repository
  /// </summary>
  /// <param name="entity">The entity to add</param>
  /// <returns>The added entity with its unique identifier</returns>
  Task<TEntity> AddAsync(TEntity entity);

  /// <summary>
  /// Add a set of entities asyncronously to the repository
  /// </summary>
  /// <param name="entities">The set of entity to add</param>
  /// <returns>A count of added entity</returns>
  Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

  /// <summary>
  /// Update an entity asyncronously into the repository
  /// </summary>
  /// <param name="entity">The entity to update</param>
  /// <returns>?The updated entity</returns>
  Task<TEntity> UpdateAsync(TEntity entity);

  /// <summary>
  /// Get an entity asyncronously from the repository from its Id
  /// </summary>
  /// <param name="id">The unique identifier of the entity to get</param>
  /// <returns>An antity or null if not found</returns>
  Task<TEntity?> GetByIdAsync(int id);

  /// <summary>
  /// Get all entity asyncronously from the repository
  /// </summary>
  /// <returns>A queryable list of entity</returns>
  IQueryable<TEntity> GetAll();

  /// <summary>
  /// Get an entity asyncronously from the repository using open query
  /// </summary>
  /// <param name="expression">The expression representing the filter query</param>
  /// <returns>A queryable list of entity</returns>
  IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

  /// <summary>
  /// Remove an entity asyncronously from the repository
  /// </summary>
  /// <param name="id">The unique identifier of the entity to remove</param>
  /// <returns>1 if the entity was removed, otherwise 0</returns>
  Task<int> RemoveAsync(int id);

  /// <summary>
  /// Remove a set of entity asyncronously from the repository
  /// </summary>
  /// <param name="entities">The set of entity to remove</param>
  /// <returns>A count of removed entity</returns>
  Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);
}
