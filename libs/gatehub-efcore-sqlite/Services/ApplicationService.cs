using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.Domain.Models;

namespace NineteenSevenFour.Gatehub.EFCore.SQLite.Services;

public class ApplicationService : IApplicationService
{
  public GateApplicationMetadata[] GetAll()
  {
    return Array.Empty<GateApplicationMetadata>();
  }
}
