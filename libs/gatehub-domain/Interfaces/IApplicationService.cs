using NineteenSevenFour.Gatehub.Domain.Models;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces;

public interface IApplicationService
{
  GateApplicationMetadata[] GetAll();
}
