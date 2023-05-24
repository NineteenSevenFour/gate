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
  public virtual Task Add(TModel model)
  {
    var entity = mapper.Map<TEntity>(model);
    return this.repository.Add(entity);
  }

  /// <inheritdoc />
  public virtual Task AddRange(IEnumerable<TModel> models)
  {
    var entities = mapper.Map<TEntity[]>(models);
    return this.repository.AddRange(entities);
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
  public virtual IQueryable<TModel> GetAll()
  {
    var entities = this.repository.GetAll();
    var model = entities.ProjectTo<TModel>(mapper.ConfigurationProvider);
    return (IQueryable<TModel>)model;
  }

  /// <inheritdoc />
  public virtual Task<TModel> GetById(int id)
  {
    var entity = this.repository.GetById(id);
    var model = mapper.Map<TModel>(entity);
    return Task.FromResult(model);
  }

  /// <inheritdoc />
  public virtual Task Remove(TModel model)
  {
    var entity = mapper.Map<TEntity>(model);
    return this.repository.Remove(entity);
  }

  /// <inheritdoc />
  public virtual Task RemoveRange(IEnumerable<TModel> models)
  {
    var entities = mapper.Map<TEntity[]>(models);
    return this.repository.RemoveRange(entities);
  }

  /// <inheritdoc />
  public virtual Task Update(TModel model)
  {
    var entity = mapper.Map<TEntity>(model);
    var newEntity = this.repository.Update(entity);
    var newModel = mapper.Map<TModel>(newEntity);
    return Task.FromResult(newModel);
  }
}
