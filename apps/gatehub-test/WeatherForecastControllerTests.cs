using FluentAssertions;

using Microsoft.Extensions.Logging;
using Moq;

using NineteenSevenFour.Gatehub.Controllers;

namespace NineteenSevenFour.Gatehub.Test;

public class WeatherForecastControllerTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void Get_ShouldReturn_AListOf_WeatherForecast()
  {
    // Arrange
    var mock = new Mock<ILogger<WeatherForecastController>>();
    ILogger<WeatherForecastController> logger = mock.Object;

    //or use this short equivalent
    logger = Mock.Of<ILogger<WeatherForecastController>>();

    var controller = new WeatherForecastController(logger);

    // Act
    var forecasts = controller.Get();

    // Assert
    forecasts?.Count().Should().Be(5);
  }
}
