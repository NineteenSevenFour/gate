using NineteenSevenFour.Gatehub.Domain.Entities;

namespace NineteenSevenFour.Gatehub.Domain.Interfaces.Repository;

public interface IRepository<T> where T : BaseEntity
{
  T[] GetAll();
}

