using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

using NineteenSevenFour.Gatehub.Controllers;
using NineteenSevenFour.Gatehub.Domain.Interfaces;

namespace NineteenSevenFour.Gatehub.Tests.Controllers;

public class ApplicationControllerTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void List_ShouldReturn_Status200OK_OnSucceess()
  {
    // Arrange
    var logger = new Mock<ILogger<ApplicationController>>().Object;
    var service = new Mock<IApplicationService>().Object;
    var controller = new ApplicationController(logger, service);

    // Act
    var actionResult = controller.List();

    // Assert
    var okObjectResult = actionResult as ObjectResult;
    Assert.IsNotNull(okObjectResult);
    okObjectResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
  }


  [Test]
  public void List_ShouldReturn_Status500InternalServerError_OnError()
  {
    // Arrange
    var errorMessage = "Expected exception from moq";
    var logger = new Mock<ILogger<ApplicationController>>().Object;
    var service = new Mock<IApplicationService>();

    service.Setup(item => item.GetAll())
           .Throws(new Exception(errorMessage));
    var controller = new ApplicationController(logger, service.Object);

    // Act
    var actionResult = controller.List();

    // Assert
    var errorObjectResult = actionResult as ObjectResult;
    Assert.IsNotNull(errorObjectResult);
    errorObjectResult?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    var problemDetail = errorObjectResult?.Value as ProblemDetails;
    Assert.IsNotNull(problemDetail);
    problemDetail?.Detail.Should().Contain(errorMessage);
  }
}
