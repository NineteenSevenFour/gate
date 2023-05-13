using FluentAssertions;

using Moq;
using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces.Repository;
using NineteenSevenFour.Gatehub.EFCore.Services;

namespace NineteenSevenFour.Gatehub.EFCore.Test.Services;

public class ApplicationServiceTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAll_ShouldReturn_AnEmptyListOf_WhenNoAppRegistered()
  {
    // Arrange
    var expectedResult = Array.Empty<GateApplicationMetadataEntity>();
    var repositoryMock = new Mock<IRepository<GateApplicationMetadataEntity>>();
    repositoryMock.Setup((r) => r.GetAll()).Returns(expectedResult);
    var serviceMock = new Mock<ApplicationService>(repositoryMock.Object);

    // Act
    var results = serviceMock.Object.GetAll();

    // Assert
    Assert.IsNotNull(results);
    results.Should().BeEquivalentTo(expectedResult);
  }
}
