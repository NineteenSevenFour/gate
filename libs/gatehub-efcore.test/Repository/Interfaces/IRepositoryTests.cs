using FluentAssertions;

using Moq;

using NineteenSevenFour.Gatehub.Domain.Models;
using NineteenSevenFour.Gatehub.EFCore.Repository.Interfaces;

namespace NineteenSevenFour.Gatehub.EFCore.Test.Repository.Interfaces;

public class IRepositoryTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAll_Should_Exists()
  {
    var service = new Mock<IRepository<GateApplicationMetadata>>().Object;

    var results = service.GetAll();

    results.Should().BeEmpty();
  }
}
