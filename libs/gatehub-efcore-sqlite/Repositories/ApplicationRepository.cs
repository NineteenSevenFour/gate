using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces.Repository;

namespace NineteenSevenFour.Gatehub.EFCore.Repositories;

public class ApplicationRepository : IRepository<GateApplicationMetadataEntity>
{
  public GateApplicationMetadataEntity[] GetAll()
  {
    return Array.Empty<GateApplicationMetadataEntity>();
  }
}
