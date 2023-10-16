using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Domain.Models;

/// <summary>
/// GATE application metadata registration model
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Basic model")]
public class GateApplicationMetadataRegistrationModel
{
  /// <summary>
  /// Name of the GATE application, must be unique across all application
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// GATE application description
  /// </summary>
  public string Description { get; set; }

  /// <summary>
  /// GATE application icon for the web app menu
  /// </summary>
  public string Icon { get; set; }
}
