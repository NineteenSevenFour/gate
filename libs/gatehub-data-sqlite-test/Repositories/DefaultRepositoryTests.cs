using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Data.sqlite.Repositories;
using NineteenSevenFour.Gatehub.Data.sqlite.Context;

namespace NineteenSevenFour.Gatehub.Data.sqlite.Test.Repositories;

public class DefaultRepositoryTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAll_ShouldReturn_AnEmptyListOf_WhenNoAppRegistered()
  {
    // Arrange
    var expectedResult = Array.Empty<GateApplicationMetadataEntity>().AsQueryable();

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    var repositoryMock = new Mock<DefaultRepository<GateApplicationMetadataEntity>>(logFactoryMoq.Object, contextMoq.Object);
    repositoryMock
      .Setup((r) => r.GetAll())
      .Returns(expectedResult);

    // Act
    var results = repositoryMock.Object.GetAll();

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(expectedResult);
  }
}
