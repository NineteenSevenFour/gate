using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.Domain.Models;
using NineteenSevenFour.Gatehub.Business.Services;
using AutoMapper;
using NineteenSevenFour.Gatehub.Data.Sqlite.Context.MappingProfile;
using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Business.Test.Services;

public class DefaultServiceTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAllShouldReturnAnEmptyListWhenNoApplicationAreRegistered()
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
  public void GetAllShouldReturnAListOfApplicationWhenAtLeastOneApplicationIsRegistered()
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
  public async Task AddAsyncShouldReturnAnApplicationModelWhenSingleApplicationRegistrationSucceed()
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
  public async Task AddRangeAsyncShouldReturnACountOfRegisterdApplicationWhenMultiApplicationRegistrationSucceed()
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

  [Test]
  public void UpdateAsyncShouldThrowNotImplementedException()
  {
    // Arrange
    GateApplicationMetadataModel inputModel = new()
    {
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
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts

    // Assert
    Assert.ThrowsAsync<NotImplementedException>(async () => await service.UpdateAsync(inputModel));
  }

  [Test]
  public async Task GetByIdAsyncShouldReturnAnApplicationModelWhenIdExists()
  {
    // Arrange
    GateApplicationMetadataEntity entity = new()
    {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };
    GateApplicationMetadataModel model = new()
    {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };
    int searchedId = 1;

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(entity);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.GetByIdAsync(searchedId);

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(model);
  }

  [Test]
  public async Task GetByIdAsyncShouldReturnNullWhenIdDoesnNotExists()
  {
    // Arrange
    GateApplicationMetadataEntity? entity = null;
    int searchedId = 1;

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(entity);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.GetByIdAsync(searchedId);

    // Assert
    Assert.That(results, Is.Null);
  }

  [Test]
  public void FindShouldReturnAnApplicationModelWhenExpressionMatchAnyRegisteredApplication()
  {
    // Arrange
    GateApplicationMetadataEntity[] entities = {
      new()
      {
        Id = 1,
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
      }
    };
    GateApplicationMetadataModel[] model = {
      new()
      {
        Id = 1,
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
        .Setup((r) => r.Find(It.IsAny<Expression<Func<GateApplicationMetadataEntity, bool>>>()))
        .Returns(entities.AsQueryable());
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = service.Find((model) => model.Id == 1);

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(model);
  }

  [Test]
  public async Task RemoveAsyncShouldReturnACountOfUnregisterdApplicationWhenSingleUnregistationSucceed()
  {
    // Arrange
    int removedEntitiesCount = 1;
    int id = 3;

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.RemoveAsync(It.IsAny<int>()))
        .ReturnsAsync(removedEntitiesCount);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.RemoveAsync(id);

    // Assert
    results.Should().Be(removedEntitiesCount);
  }

  [Test]
  public async Task RemoveAsyncShouldReturnZeroWhenSingleApplicationUnregistrationFails()
  {
    // Arrange
    int removedEntitiesCount = 0;
    int id = 3;

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var mapperMoq = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile(new GateApplicationMetadataMappingProfile());
    });
    var mapper = mapperMoq.CreateMapper();

    var repositoryMoq = new Mock<IDefaultRepository<GateApplicationMetadataEntity>>();
    repositoryMoq
        .Setup((r) => r.RemoveAsync(It.IsAny<int>()))
        .ReturnsAsync(removedEntitiesCount);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.RemoveAsync(id);

    // Assert
    results.Should().Be(removedEntitiesCount);
  }

  [Test]
  public async Task RemoveRangeAsyncShouldReturnACountOfUnregisterdApplicationWhenMultiApplicationUnregistrationSucceed()
  {
    // Arrange
    int removedEntitiesCount = 1;
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
        .Setup((r) => r.RemoveRangeAsync(It.IsAny<GateApplicationMetadataEntity[]>()))
        .ReturnsAsync(removedEntitiesCount);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.RemoveRangeAsync(inputModels);

    // Assert
    results.Should().Be(removedEntitiesCount);
  }

  [Test]
  public async Task RemoveRangeAsyncShouldReturnZeroWhenMultiApplicationUnregistrationFails()
  {
    // Arrange
    int removedEntitiesCount = 0;
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
        .Setup((r) => r.RemoveRangeAsync(It.IsAny<GateApplicationMetadataEntity[]>()))
        .ReturnsAsync(removedEntitiesCount);
    var service = new DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>(logFactoryMoq.Object, mapper, repositoryMoq.Object);

    // Acts
    var results = await service.RemoveRangeAsync(inputModels);

    // Assert
    results.Should().Be(removedEntitiesCount);
  }
}
