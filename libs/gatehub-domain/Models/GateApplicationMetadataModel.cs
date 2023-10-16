using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Domain.Models;

/// <summary>
/// GATE application metadata model
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Basic model")]
public class GateApplicationMetadataModel : GateApplicationMetadataRegistrationModel
{
  /// <summary>
  /// Constructor of the GATE application registered model
  /// </summary>
  public GateApplicationMetadataModel()
  {
  }

  /// <summary>
  /// Constructor of the GATE application registered model
  /// </summary>
  /// <param name="name">GATE application unique name</param>
  /// <param name="description">GATE application descriptive summary, goal and main features</param>
  /// <param name="icon">GATE application icon code (from available list)</param>
  public GateApplicationMetadataModel(string name, string description, string icon)
  {
    this.Name = name;
    this.Description = description;
    this.Icon = icon;
  }

  /// <summary>
  /// Constructor of the GATE application registered model
  /// </summary>
  /// <param name="newApp"><see langword="abstract"/>GATE applicaton registration model</param>
  public GateApplicationMetadataModel(GateApplicationMetadataRegistrationModel newApp) : this(newApp.Name, newApp.Description, newApp.Icon)
  {
  }

  /// <summary>
  /// Constructor of the GATE application registered model
  /// </summary>
  /// <param name="id">GATE application unique identifier</param>
  /// <param name="name">GATE application unique name</param>
  /// <param name="description">GATE application descriptive summary, goal and main features</param>
  /// <param name="icon">GATE application icon code (from available list)</param>
  public GateApplicationMetadataModel(int id, string name, string description, string icon) : this(name, description, icon)
  {
    this.Id = id;
  }

  /// <summary>
  /// GATE application unique identifier
  /// </summary>
  public int Id { get; set; }
}
