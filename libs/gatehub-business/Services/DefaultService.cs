using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.Extensions.Logging;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.Domain.Models;
using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Business.Services;

public class DefaultService<TModel, TEntity> : IDefaultService<TModel>
  where TModel : BaseModel
  where TEntity : BaseEntity
{
  protected readonly ILogger<DefaultService<TModel, TEntity>> logger;
  protected readonly IMapper mapper;
  protected readonly IDefaultRepository<TEntity> repository;

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
    var model = entities.ProjectTo<TModel>(mapper.ConfigurationProvider);
    return (IQueryable<TModel>)model;
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
