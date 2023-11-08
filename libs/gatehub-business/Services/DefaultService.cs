using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.Extensions.Logging;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Business.Services;

/// <summary>
/// Default service with CRUD implementation
/// </summary>
/// <typeparam name="TModel">The model storing data used by client application</typeparam>
/// <typeparam name="TEntity">The entity used by the backend DB</typeparam>
public class DefaultService<TModel, TEntity> : IDefaultService<TModel>
  where TModel : class, new()
  where TEntity : BaseEntity
{
  /// <summary>
  /// The logger
  /// </summary>
  protected readonly ILogger<DefaultService<TModel, TEntity>> logger;

  /// <summary>
  /// The automapper instance
  /// </summary>
  protected readonly IMapper mapper;

  /// <summary>
  /// The DB repository implementation
  /// </summary>
  protected readonly IDefaultRepository<TEntity> repository;

  /// <summary>
  /// Default cTor
  /// </summary>
  /// <param name="loggerFactory">Logger factory</param>
  /// <param name="mapper">AutoMapper instance</param>
  /// <param name="repository">Repository instance</param>
  public DefaultService(ILoggerFactory loggerFactory, IMapper mapper, IDefaultRepository<TEntity> repository)
  {
    this.logger = loggerFactory.CreateLogger<DefaultService<TModel, TEntity>>();
    this.mapper = mapper;
    this.repository = repository;
  }

  /// <inheritdoc />
  public virtual async Task<TModel> AddAsync(TModel model)
  {
    var entity = mapper.Map<TEntity>(model);
    var newEntity = await this.repository.AddAsync(entity);
    return mapper.Map<TModel>(newEntity);
  }

  /// <inheritdoc />
  public virtual async Task<int> AddRangeAsync(IEnumerable<TModel> models)
  {
    var entities = mapper.Map<TEntity[]>(models);
    return await this.repository.AddRangeAsync(entities);
  }

  /// <inheritdoc />
  public Task<TModel> UpdateAsync(TModel model)
  {
    throw new NotImplementedException();
  }

  /// <inheritdoc />
  public virtual async Task<TModel?> GetByIdAsync(int id)
  {
    var entity = await this.repository.GetByIdAsync(id);
    return mapper.Map<TModel>(entity);
  }

  /// <inheritdoc />
  public virtual IQueryable<TModel> GetAll()
  {
    var entities = this.repository.GetAll();
    return entities.ProjectTo<TModel>(mapper.ConfigurationProvider);
  }

  /// <inheritdoc />
  public virtual IQueryable<TModel> Find(Expression<Func<TModel, bool>> expression)
  {
    var predicate = this.mapper.Map<Expression<Func<TEntity, bool>>>(expression);
    var entities = this.repository.Find(predicate);
    var models = entities.ProjectTo<TModel>(mapper.ConfigurationProvider);
    return models;
  }

  /// <inheritdoc />
  public virtual async Task<int> RemoveAsync(int id)
  {
    return await this.repository.RemoveAsync(id);
  }

  /// <inheritdoc />
  public virtual async Task<int> RemoveRangeAsync(IEnumerable<TModel> models)
  {
    var entities = mapper.Map<TEntity[]>(models);
    return await this.repository.RemoveRangeAsync(entities);
  }
}
