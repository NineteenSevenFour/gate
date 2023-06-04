using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Data.Sqlite.Repositories;
using NineteenSevenFour.Gatehub.Data.Sqlite.Context;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq.Expressions;

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Test.Repositories;

public class DefaultRepositoryTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAllShouldReturnAnEmptyListWhenNoApplicationAreRegistered()
  {
    // Arrange
    var expectedResult = Array.Empty<GateApplicationMetadataEntity>().AsQueryable();

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(c => c.Set<GateApplicationMetadataEntity>()).ReturnsDbSet(expectedResult);
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Act
    var results = repository.GetAll();

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(expectedResult);
  }

  [Test]
  public void GetAllShouldReturnAListOfApplicationWhenAtLeastOneApplicationIsRegistered()
  {
    // Arrange
    GateApplicationMetadataEntity[] expectedResult = {
      new GateApplicationMetadataEntity()
      {
        Id = 1,
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
      },
      new GateApplicationMetadataEntity()
      {
        Id = 2,
        Name = "AppTwo",
        Description = "App two",
        Icon = "Cog"
      }
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(c => c.Set<GateApplicationMetadataEntity>()).ReturnsDbSet(expectedResult);
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var results = repository.GetAll();

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(expectedResult);
  }

  [Test]
  public async Task AddAsyncShouldReturnAnApplicationModelWhenSingleApplicationRegistrationSucceed()
  {
    // Arrange
    GateApplicationMetadataEntity input = new()
    {
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };
    GateApplicationMetadataEntity output = new()
    {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
#pragma warning disable EF1001 // Internal EF Core API usage.
    var entityEntryMoq = new Mock<EntityEntry<GateApplicationMetadataEntity>>(It.IsAny<InternalEntityEntry>());
#pragma warning restore EF1001 // Internal EF Core API usage.
    entityEntryMoq.SetupGet(c => c.Entity).Returns(output);
    contextMoq.Setup(m => m.AddAsync(It.IsAny<GateApplicationMetadataEntity>(), default))
              .ReturnsAsync(entityEntryMoq.Object);
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(1)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var result = await repository.AddAsync(input);

    // Assert
    Assert.That(result, Is.Not.Null);
    result.Should().BeEquivalentTo(output);
  }

  [Test]
  public void AddAsyncShouldThrowArgumentNullExceptionWhenNullArgumentGiven()
  {
    // Arrange
    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddAsync(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
  }

  [Test]
  public void AddAsyncShouldThrowDbUpdateExceptionWhenSingleApplicationRegistrationFails()
  {
    // Arrange
    GateApplicationMetadataEntity input = new()
    {
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
#pragma warning disable EF1001 // Internal EF Core API usage.
    var entityEntryMoq = new Mock<EntityEntry<GateApplicationMetadataEntity>>(It.IsAny<InternalEntityEntry>());
#pragma warning restore EF1001 // Internal EF Core API usage.
    contextMoq.Setup(m => m.AddAsync(It.IsAny<GateApplicationMetadataEntity>(), default));
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(0)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
    Assert.ThrowsAsync<DbUpdateException>(async () => await repository.AddAsync(input));
  }

  [Test]
  public async Task AddRangeAsyncShouldReturnACountOfRegisterdApplicationWhenMultiApplicationRegistrationSucceed()
  {
    // Arrange
    GateApplicationMetadataEntity[] input = {
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
    var output = input.Length;

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(m => m.AddRangeAsync(It.IsAny<GateApplicationMetadataEntity[]>(), default));
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(output)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var results = await repository.AddRangeAsync(input);

    // Assert
    results.Should().Be(output);
  }

  [Test]
  public void AddRangeAsyncShouldThrowArgumentNullExceptionWhenNullArgumentGiven()
  {
    // Arrange
    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddRangeAsync(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
  }

  [Test]
  public void AddRangeAsyncShouldThrowDbUpdateExceptionWhenSingleApplicationRegistrationFails()
  {
    // Arrange
    GateApplicationMetadataEntity[] input = {
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

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
#pragma warning disable EF1001 // Internal EF Core API usage.
    var entityEntryMoq = new Mock<EntityEntry<GateApplicationMetadataEntity>>(It.IsAny<InternalEntityEntry>());
#pragma warning restore EF1001 // Internal EF Core API usage.
    contextMoq.Setup(m => m.AddAsync(It.IsAny<GateApplicationMetadataEntity>(), default));
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(0)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
    Assert.ThrowsAsync<DbUpdateException>(async () => await repository.AddRangeAsync(input));
  }

  [Test]
  public void UpdateAsyncShouldThrowArgumentNullExceptionWhenNullArgumentGiven()
  {
    // Arrange
    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.UpdateAsync(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
  }

  [Test]
  public void UpdateShouldThrowDbUpdateExceptionWhenApplicationRegistrationUpdateFails()
  {
    // Arrange
    GateApplicationMetadataEntity input = new()
    {
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
#pragma warning disable EF1001 // Internal EF Core API usage.
    var entityEntryMoq = new Mock<EntityEntry<GateApplicationMetadataEntity>>(It.IsAny<InternalEntityEntry>());
#pragma warning restore EF1001 // Internal EF Core API usage.
    contextMoq.Setup(m => m.Update(It.IsAny<GateApplicationMetadataEntity>()));
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(0)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
    Assert.ThrowsAsync<DbUpdateException>(async () => await repository.UpdateAsync(input));
  }

  [Test]
  public async Task UpdateAsyncShouldReturnUpdatedEntity()
  {
    // Arrange
    GateApplicationMetadataEntity entity =
      new()
      {
        Id = 2,
        Name = "updatedApp",
        Description = "Updated App",
        Icon = "updated Icon"
      };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
#pragma warning disable EF1001 // Internal EF Core API usage.
    var entityEntryMoq = new Mock<EntityEntry<GateApplicationMetadataEntity>>(It.IsAny<InternalEntityEntry>());
#pragma warning restore EF1001 // Internal EF Core API usage.
    entityEntryMoq.SetupGet(c => c.Entity).Returns(entity);
    contextMoq.Setup(m => m.Update(It.IsAny<GateApplicationMetadataEntity>()))
              .Returns(entityEntryMoq.Object);
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(1)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var result = await repository.UpdateAsync(entity);

    // Assert
    //Assert.That(result, Is.Not.Null);
    result.Should().BeEquivalentTo(entity);
  }

  [Test]
  public async Task GetByIdAsyncShouldReturnAnApplicationModelWhenIdExists()
  {
    // Arrange
    var appList = new List<GateApplicationMetadataEntity>() {
      new GateApplicationMetadataEntity()
      {
        Id = 1,
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
      },
      new GateApplicationMetadataEntity()
      {
        Id = 2,
        Name = "AppTwo",
        Description = "App one",
        Icon = "Cog"
      }
    };
    int searchedId = 2;
    var expected = appList.FirstOrDefault((a) => a.Id == searchedId);

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(m => m.FindAsync<GateApplicationMetadataEntity>(It.IsAny<object?[]?>()))
              .ReturnsAsync(expected)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var results = await repository.GetByIdAsync(searchedId);

    // Assert
    results.Should().BeEquivalentTo(expected);
  }

  [Test]
  public async Task GetByIdAsyncShouldReturnNullWhenIdDoesnNotExists()
  {
    // Arrange
    var appList = new List<GateApplicationMetadataEntity>() {
      new GateApplicationMetadataEntity()
      {
        Id = 1,
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
      },
      new GateApplicationMetadataEntity()
      {
        Id = 2,
        Name = "AppTwo",
        Description = "App one",
        Icon = "Cog"
      }
    };
    int searchedId = 3;
    var expected = appList.FirstOrDefault((a) => a.Id == searchedId);

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(m => m.FindAsync<GateApplicationMetadataEntity>(It.IsAny<object?[]?>()))
              .ReturnsAsync(expected)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var results = await repository.GetByIdAsync(searchedId);

    // Assert
    results.Should().BeEquivalentTo(expected);
  }

  [Test]
  public void FindShouldThrowArgumentNullExceptionWhenNullArgumentGiven()
  {
    // Arrange
    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    Assert.Throws<ArgumentNullException>(() => repository.Find(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
  }

  [Test]
  public void FindShouldReturnAListOfApplicationWhenExpressionMatchesAnyRegisteredApplication()
  {
    // Arrange
    GateApplicationMetadataEntity[] expectedResult = {
      new GateApplicationMetadataEntity()
      {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
      }
    };

    Expression<Func<GateApplicationMetadataEntity, bool>> expr = (entity) => entity.Id == 1;

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(c => c.Set<GateApplicationMetadataEntity>()).ReturnsDbSet(expectedResult);
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var results = repository.Find(expr);

    // Assert
    Assert.That(results, Is.Not.Null);
    results.Should().BeEquivalentTo(expectedResult.AsQueryable());
  }

  [Test]
  public async Task RemoveAsyncShouldReturnCountOfUnregisterdApplicationWhenSingleApplicationUnregistrationSucceed()
  {
    // Arrange
    GateApplicationMetadataEntity entity = new()
    {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
#pragma warning disable EF1001 // Internal EF Core API usage.
    var entityEntryMoq = new Mock<EntityEntry<GateApplicationMetadataEntity>>(It.IsAny<InternalEntityEntry>());
#pragma warning restore EF1001 // Internal EF Core API usage.
    entityEntryMoq.SetupGet(c => c.Entity).Returns(entity);
    contextMoq.Setup(m => m.Remove(It.IsAny<int>()))
              .Returns(entityEntryMoq.Object);
    contextMoq.Setup(m => m.FindAsync<GateApplicationMetadataEntity>(It.IsAny<object?[]?>()))
              .ReturnsAsync(entity)
              .Verifiable();
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(1)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var result = await repository.RemoveAsync(entity.Id);

    // Assert
    result.Should().Be(1);
  }

  [Test]
  public void RemoveAsyncShouldThrowArgumentNullExceptionWhenNullArgumentGiven()
  {
    // Arrange
    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(m => m.FindAsync<GateApplicationMetadataEntity>(It.IsAny<object?[]?>()))
              .ReturnsAsync(null as GateApplicationMetadataEntity)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
    Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await repository.RemoveAsync(0));
  }

  [Test]
  public void RemoveAsyncShouldThrowDbUpdateExceptionWhenSingleApplicationUnregistrationFails()
  {
    // Arrange
    GateApplicationMetadataEntity entity = new()
    {
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(m => m.Remove(It.IsAny<int>()));
    contextMoq.Setup(m => m.FindAsync<GateApplicationMetadataEntity>(It.IsAny<object?[]?>()))
              .ReturnsAsync(entity)
              .Verifiable();
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(0)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
    Assert.ThrowsAsync<DbUpdateException>(async () => await repository.RemoveAsync(entity.Id));
  }

  [Test]
  public async Task RemoveRangeAsyncShouldReturnACountOfUnregisterdApplicationWhenMultiApplicationUnregistrationSucceed()
  {
    // Arrange
    GateApplicationMetadataEntity[] input = {
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
    var output = input.Length;

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(output)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts
    var result = await repository.RemoveRangeAsync(input);

    // Assert
    result.Should().Be(output);
  }

  [Test]
  public void RemoveRangeAsyncShouldThrowArgumentNullExceptionWhenNullArgumentGiven()
  {
    // Arrange
    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.RemoveRangeAsync(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
  }

  [Test]
  public void RemoveRangeAsyncShouldThrowDbUpdateExceptionWhenMultiApplicationUnregistrationFails()
  {
    // Arrange
    GateApplicationMetadataEntity[] input = {
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

    var logFactoryMoq = new Mock<ILoggerFactory>();
    var contextMoq = new Mock<SqliteDbContext>();
    contextMoq.Setup(m => m.RemoveRange(It.IsAny<int>()));
    contextMoq.Setup(c => c.SaveChangesAsync(default))
              .ReturnsAsync(0)
              .Verifiable();
    var repository = new DefaultRepository<GateApplicationMetadataEntity>(logFactoryMoq.Object, contextMoq.Object);

    // Acts

    // Assert
    Assert.ThrowsAsync<DbUpdateException>(async () => await repository.RemoveRangeAsync(input));
  }
}
