using Moq;
using NineteenSevenFour.Gatehub.EFCore.SQLite.Services;

namespace NineteenSevenFour.Gatehub.EFCore.SQLite.Test.Services;

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
    var service = new Mock<ApplicationService>().Object;

    // Act
    var results = service.GetAll();

    // Assert
    Assert.IsNotNull(results);
  }
}
