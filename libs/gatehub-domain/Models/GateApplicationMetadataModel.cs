using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Domain.Models;

/// <summary>
/// GATE application metadata model
/// </summary>
[ExcludeFromCodeCoverage]
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
  /// <param name="name"></param>
  /// <param name="description"></param>
  /// <param name="icon"></param>
  public GateApplicationMetadataModel(string name, string description, string icon)
  {
    this.Name = name;
    this.Description = description;
    this.Icon = icon;
  }

  public GateApplicationMetadataModel(GateApplicationMetadataRegistrationModel newApp) : this(newApp.Name, newApp.Description, newApp.Icon)
  {
  }

  /// <summary>
  /// cTor
  /// </summary>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <param name="description"></param>
  /// <param name="icon"></param>
  public GateApplicationMetadataModel(int id, string name, string description, string icon) : this(name, description, icon)
  {
    this.Id = id;
  }

  /// <summary>
  /// The ID of the GATE Application
  /// </summary>
  public int Id { get; set; }
}
