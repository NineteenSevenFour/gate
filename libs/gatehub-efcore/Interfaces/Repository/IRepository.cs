namespace NineteenSevenFour.Gatehub.EFCore.Repository.Interfaces;

public interface IRepository<T> where T : class
{
  T[] GetAll();
}

