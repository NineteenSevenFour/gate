using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Domain.Entities;

/// <summary>
/// Abstract base entity
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class BaseEntity
{
  /// <summary>
  /// Entity instance ID
  /// </summary>
  public int Id { get; set; }
}
