using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Domain.Models;

/// <summary>
/// GATE application metadata model
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Basic model")]
public class GateApplicationMetadataModel : GateApplicationMetadataRegistrationModel
{
  /// <summary>
  /// cTor
  /// </summary>
  public GateApplicationMetadataModel()
  {
  }

  /// <summary>
  /// cTor
  /// </summary>
  /// <param name="name">Unique name of the GATE application</param>
  /// <param name="description">Descriptive summary of the GATE application goal and main features</param>
  /// <param name="icon">The GATE application icon code (from available list)</param>
  public GateApplicationMetadataModel(string name, string description, string icon)
  {
    this.Name = name;
    this.Description = description;
    this.Icon = icon;
  }

  /// <summary>
  /// cTor
  /// </summary>
  /// <param name="newApp"><see langword="abstract"/>GATE applicaton registration model</param>
  public GateApplicationMetadataModel(GateApplicationMetadataRegistrationModel newApp) : this(newApp.Name, newApp.Description, newApp.Icon)
  {
  }

  /// <summary>
  /// cTor
  /// </summary>
  /// <param name="id">The GATE application unique identifier</param>
  /// <param name="name">Unique name of the GATE application</param>
  /// <param name="description">Descriptive summary of the GATE application goal and main features</param>
  /// <param name="icon">The GATE application icon code (from available list)</param>
  public GateApplicationMetadataModel(int id, string name, string description, string icon) : this(name, description, icon)
  {
    this.Id = id;
  }

  /// <summary>
  /// The ID of the GATE Application
  /// </summary>
  public int Id { get; set; }
}
