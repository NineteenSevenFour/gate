using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Models;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces.Services;

public interface IService<TM, TE>
where TM : BaseModel
where TE : BaseEntity
{
  TM[] GetAll();
}
