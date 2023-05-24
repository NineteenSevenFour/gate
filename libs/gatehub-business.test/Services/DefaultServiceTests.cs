using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.Domain.Models;
using NineteenSevenFour.Gatehub.Business.Services;
using AutoMapper;

namespace NineteenSevenFour.Gatehub.Business.Test.Services;

public class DefaultServiceTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAll_ShouldReturn_AnEmptyListOf_WhenNoAppRegistered()
  {
    // Arrange
    var expectedResult = Array.Empty<GateApplicationMetadataModel>().AsQueryable();

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMock = new Mock<IMapper>();
    var repositoryMock = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    var serviceMock = new Mock<DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>>(logFactoryMoq.Object, mapperMock.Object, repositoryMock.Object);
    serviceMock
        .Setup((r) => r.GetAll())
        .Returns((IQueryable<GateApplicationMetadataModel>)Array.Empty<GateApplicationMetadataModel>().AsQueryable());

    // Acts
    var results = serviceMock.Object.GetAll();

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(expectedResult);
  }
}
