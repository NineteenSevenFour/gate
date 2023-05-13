using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NineteenSevenFour.Gatehub.Controllers;
using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces.Services;
using NineteenSevenFour.Gatehub.Domain.Models;

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
    var service = new Mock<IService<GateApplicationMetadataModel, GateApplicationMetadataEntity>>().Object;
    var controller = new ApplicationController(logger, service);

    // Act
    var actionResult = controller.List();

    // Assert
    var okObjectResult = actionResult as ObjectResult;
    Assert.That(okObjectResult, Is.Not.Null);
    okObjectResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
  }

  [Test]
  public void List_ShouldReturn_Status500InternalServerError_OnError()
  {
    // Arrange
    var errorMessage = "Expected exception from moq";
    var logger = new Mock<ILogger<ApplicationController>>().Object;
    var service = new Mock<IService<GateApplicationMetadataModel, GateApplicationMetadataEntity>>();

    service.Setup(item => item.GetAll())
           .Throws(new Exception(errorMessage));
    var controller = new ApplicationController(logger, service.Object);

    // Act
    var actionResult = controller.List();

    // Assert
    var errorObjectResult = actionResult as ObjectResult;
    Assert.That(errorObjectResult, Is.Not.Null);
    errorObjectResult?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    var problemDetail = errorObjectResult?.Value as ProblemDetails;
    Assert.That(problemDetail, Is.Not.Null);
    problemDetail?.Detail.Should().Contain(errorMessage);
  }
}
