using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Domain.Models;

/// <summary>
/// Abstract base model
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class BaseModel
{
  /// <summary>
  /// Model instance ID
  /// </summary>
  public int Id { get; set; }
}
