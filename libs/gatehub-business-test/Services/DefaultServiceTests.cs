using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.Domain.Models;
using NineteenSevenFour.Gatehub.Business.Services;
using AutoMapper;
using NineteenSevenFour.Gatehub.Data.Sqlite.Context.MappingProfile;

namespace NineteenSevenFour.Gatehub.Business.Test.Services;

public class DefaultServiceTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAll_ShouldReturn_AnEmptyList_WhenNoApplicationAreRegistered()
  {
    // Arrange
    GateApplicationMetadataEntity[] entities = Array.Empty<GateApplicationMetadataEntity>();
    GateApplicationMetadataModel[] models = Array.Empty<GateApplicationMetadataModel>();

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.GetAll())
        .Returns(entities.AsQueryable());
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = service.GetAll();

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(models);
  }

  [Test]
  public void GetAll_ShouldReturn_AListOfApplication_WhenApplicationAreRegistered()
  {
    // Arrange
    GateApplicationMetadataEntity[] entities = {
      new GateApplicationMetadataEntity()
      {
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
      },
      new GateApplicationMetadataEntity()
      {
        Name = "AppTwo",
        Description = "App two",
        Icon = "Cog"
      }
    };
    GateApplicationMetadataModel[] models = {
      new GateApplicationMetadataModel()
      {
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
      },
      new GateApplicationMetadataModel()
      {
        Name = "AppTwo",
        Description = "App two",
        Icon = "Cog"
      }
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.GetAll())
        .Returns(entities.AsQueryable());
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = service.GetAll();

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(models);
  }

  [Test]
  public async Task AddAsync_ShouldReturn_AnApplicationModel_WhenApplicationIsRegistered_Succeefuly()
  {
    // Arrange
    GateApplicationMetadataEntity savedEntity = new()
    {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };
    GateApplicationMetadataModel inputModel = new()
    {
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };
    GateApplicationMetadataModel savedModel = new()
    {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.AddAsync(It.IsAny<GateApplicationMetadataEntity>()))
        .ReturnsAsync(savedEntity);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.AddAsync(inputModel);

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(savedModel);
  }

  [Test]
  public async Task AddRangeAsync_ShouldReturn_ACountOfRegisterdApplication_WhenApplicationAreRegistered_Succeefuly()
  {
    // Arrange
    int savedEntitiesCount = 1;
    GateApplicationMetadataModel[] inputModels = {
      new()
      {
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
      }
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.AddRangeAsync(It.IsAny<GateApplicationMetadataEntity[]>()))
        .ReturnsAsync(savedEntitiesCount);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.AddRangeAsync(inputModels);

    // Assert
    results.Should().Be(savedEntitiesCount);
  }
}
