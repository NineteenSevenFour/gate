
namespace NineteenSevenFour.Gatehub.Domain.Exceptions;

/// <summary>
/// Exception raised when entity not exist in database and operation can not procced.
/// </summary>
public class EntityNotFoundException : Exception
{
  /// <summary>
  /// Initializes a new instance of the <see cref="EntityNotFoundException" /> class.
  /// </summary>
  public EntityNotFoundException(string entityName, object id, string message) : base(message)
  {
    this.EntityName = entityName;
    this.Id = id;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="EntityNotFoundException" /> class.
  /// </summary>
  public EntityNotFoundException(string entityName, object id)
      : base($"{entityName} with ID: {id} was not found")
  {
    this.EntityName = entityName;
    this.Id = id;
  }

  /// <summary>
  /// Gets the entity name.
  /// </summary>
  public string EntityName { get; }

  /// <summary>
  /// Gets the entity id.
  /// </summary>
  public object Id { get; }
}

