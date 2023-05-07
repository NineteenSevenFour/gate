using FluentAssertions;

using Moq;

using NineteenSevenFour.Gatehub.Domain.Interfaces;

namespace NineteenSevenFour.Gatehub.Domain.Test.Interfaces;

public class IApplicationServiceTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAll_Should_Exists()
  {
    // Arrange
    var service = new Mock<IApplicationService>().Object;

    // Act
    var results = service.GetAll();

    // Assert
    results.Should().BeEmpty();
  }
}
