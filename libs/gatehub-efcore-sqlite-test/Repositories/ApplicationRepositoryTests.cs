using Moq;
using NineteenSevenFour.Gatehub.EFCore.Repositories;

namespace NineteenSevenFour.Gatehub.EFCore.SQLite.Test.Repositories;

public class ApplicationRepositoryTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetAll_ShouldReturn_AnEmptyListOf_WhenNoAppRegistered()
  {
    // Arrange
    var service = new Mock<ApplicationRepository>().Object;

    // Act
    var results = service.GetAll();

    // Assert
    Assert.IsNotNull(results);
  }
}
