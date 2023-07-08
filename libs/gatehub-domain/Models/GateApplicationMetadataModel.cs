using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Domain.Models;

/// <summary>
/// GATE application metadata model
/// </summary>
[ExcludeFromCodeCoverage]
public class GateApplicationMetadataModel : GateApplicationMetadataRegistrationModel
{
  /// <summary>
  /// The ID of the GATE Application
  /// </summary>
  public int Id { get; set; }
}
