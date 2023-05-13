using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces.Repository;
using NineteenSevenFour.Gatehub.Domain.Interfaces.Services;
using NineteenSevenFour.Gatehub.Domain.Models;

namespace NineteenSevenFour.Gatehub.EFCore.Services;

public class ApplicationService : IService<GateApplicationMetadataModel, GateApplicationMetadataEntity>
{
  private IRepository<GateApplicationMetadataEntity> appRepository;

  public ApplicationService(IRepository<GateApplicationMetadataEntity> appRepo)
  {
    this.appRepository = appRepo;
  }

  public GateApplicationMetadataModel[] GetAll()
  {
    return Array.Empty<GateApplicationMetadataModel>();
    // TODO : implement automapper
    //this.appRepository.GetAll();
  }
}
