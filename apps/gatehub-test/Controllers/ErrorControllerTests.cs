using FluentAssertions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using NineteenSevenFour.Gatehub.Controllers;

using System.Net;

namespace NineteenSevenFour.Gatehub.Tests.Controllers;

public class ErrorControllerTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void HandleError_ShouldReturn_OnError()
  {
    // Arrange
    var controller = new ErrorController();

    // Act
    var actionResult = controller.HandleError();

    // Assert
    var errorObjectResult = actionResult as ObjectResult;
    var problemDetail = errorObjectResult?.Value as ProblemDetails;
    Assert.That(problemDetail, Is.Not.Null);
  }

  [Test]
  public void HandleErrorDevelopment_ShouldReturn_NotFound_WhenInProduction()
  {
    // Arrange
    var serviceProvider = new ServiceCollection()
      .AddScoped<IHostEnvironment, MockProdHostingEnvironment>()
      .BuildServiceProvider();

    var controller = new ErrorController();

    // Act
    var mockHostingEnvironment = serviceProvider.GetService<IHostEnvironment>();
#pragma warning disable CS8604 // Possible null reference argument.
    var actionResult = controller.HandleErrorDevelopment(mockHostingEnvironment);
#pragma warning restore CS8604 // Possible null reference argument.

    // Assert
    var errorObjectResult = actionResult as NotFoundResult;
    Assert.That(errorObjectResult, Is.Not.Null);
    errorObjectResult?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
  }

  [Test]
  public void HandleErrorDevelopment_ShouldReturn_NotFound_WhenInDevelopment()
  {
    // Arrange
    var errorMessage = "Some error message";
    var stackTrace = new Exception("inner exception");
    var serviceProvider = new ServiceCollection()
      .AddScoped<IHostEnvironment, MockDevHostingEnvironment>()
      .BuildServiceProvider();

    var hostEnvironmentMoq = new Mock<IHostEnvironment>();
    var controller = new ErrorController();

    controller.ControllerContext.HttpContext = new DefaultHttpContext();
    var exceptionHandlerFeatureMoq = new Mock<IExceptionHandlerFeature>();
    exceptionHandlerFeatureMoq.SetupGet(ex => ex.Error)
                              .Returns(new Exception(errorMessage, stackTrace));
    controller.ControllerContext.HttpContext.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeatureMoq.Object);

    // Act
    var mockHostingEnvironment = serviceProvider.GetService<IHostEnvironment>();
#pragma warning disable CS8604 // Possible null reference argument.
    var actionResult = controller.HandleErrorDevelopment(mockHostingEnvironment);
#pragma warning restore CS8604 // Possible null reference argument.

    // Assert
    var errorObjectResult = actionResult as ObjectResult;
    Assert.That(errorObjectResult, Is.Not.Null);

    var problemDetail = errorObjectResult?.Value as ProblemDetails;
    Assert.That(problemDetail, Is.Not.Null);
    problemDetail?.Title.Should().Be(errorMessage);
  }
}
