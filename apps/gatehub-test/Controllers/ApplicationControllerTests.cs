using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NineteenSevenFour.Gatehub.Controllers;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
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
    GateApplicationMetadataModel[] applications = {
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

    var loggerMoq = new Mock<ILogger<ApplicationController>>();
    var serviceMoq = new Mock<IDefaultService<GateApplicationMetadataModel>>();

    serviceMoq.Setup(service => service.GetAll())
               .Returns(applications.AsQueryable());
    var controller = new ApplicationController(loggerMoq.Object, serviceMoq.Object);

    // Act
    var actionResult = controller.List();

    // Assert
    var okObjectResult = actionResult as ObjectResult;
    Assert.That(okObjectResult, Is.Not.Null);
    okObjectResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
    okObjectResult?.Value.Should().BeEquivalentTo(applications);
  }

  [Test]
  public void List_ShouldReturn_Status500InternalServerError_OnError()
  {
    // Arrange
    var errorMessage = "Expected exception from moq";
    var loggerMoq = new Mock<ILogger<ApplicationController>>();
    var serviceMoq = new Mock<IDefaultService<GateApplicationMetadataModel>>();

    serviceMoq.Setup(service => service.GetAll())
               .Throws(new Exception(errorMessage));
    var controller = new ApplicationController(loggerMoq.Object, serviceMoq.Object);

    // Act
    var actionResult = controller.List();

    // Assert
    var errorObjectResult = actionResult as ObjectResult;
    Assert.That(errorObjectResult, Is.Not.Null);
    errorObjectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    var problemDetail = errorObjectResult.Value as ProblemDetails;
    Assert.That(problemDetail, Is.Not.Null);
    problemDetail.Detail.Should().Contain(errorMessage);
  }

  [Test]
  public async Task Register_ShouldReturn_Status200OK_OnSucceess()
  {
    // Arrange
    GateApplicationMetadataModel payload = new(){
        Name = "AppOne",
        Description = "App one",
        Icon = "Shield"
    };
    GateApplicationMetadataModel result = new()
    {
      Id = 1,
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };
    var loggerMoq = new Mock<ILogger<ApplicationController>>();
    var serviceMoq = new Mock<IDefaultService<GateApplicationMetadataModel>> ();

    serviceMoq.Setup(service => service.AddAsync(It.IsAny<GateApplicationMetadataModel>()))
               .ReturnsAsync(result);
    var controller = new ApplicationController(loggerMoq.Object, serviceMoq.Object);

    // Act
    var actionResult = await controller.Register(payload);

    // Assert
    var okObjectResult = actionResult as ObjectResult;
    Assert.That(okObjectResult, Is.Not.Null);
    okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    okObjectResult.Value.Should().BeEquivalentTo(result);
  }

  [Test]
  public async Task Register_ShouldReturn_Status500InternalServerError_OnError()
  {
    // Arrange
    GateApplicationMetadataModel payload = new()
    {
      Name = "AppOne",
      Description = "App one",
      Icon = "Shield"
    };
    var errorMessage = "Expected exception from moq";
    var loggerMoq = new Mock<ILogger<ApplicationController>>();
    var serviceMoq = new Mock<IDefaultService<GateApplicationMetadataModel>>();

    serviceMoq.Setup(service => service.AddAsync(It.IsAny<GateApplicationMetadataModel>()))
               .ThrowsAsync(new Exception(errorMessage));
    var controller = new ApplicationController(loggerMoq.Object, serviceMoq.Object);

    // Act
    var actionResult = controller.Register(payload);

    // Assert
    var errorObjectResult = await actionResult as ObjectResult;
    Assert.That(errorObjectResult, Is.Not.Null);
    errorObjectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    var problemDetail = errorObjectResult.Value as ProblemDetails;
    Assert.That(problemDetail, Is.Not.Null);
    problemDetail.Detail.Should().Contain(errorMessage);
  }

  [Test]
  public async Task Unregister_ShouldReturn_Status200OK_OnSucceess()
  {
    // Arrange
    int payload = 1;
    int result = 1;
    var loggerMoq = new Mock<ILogger<ApplicationController>>();
    var serviceMoq = new Mock<IDefaultService<GateApplicationMetadataModel>>();

    serviceMoq.Setup(service => service.RemoveAsync(It.IsAny<int>()))
               .ReturnsAsync(result);
    var controller = new ApplicationController(loggerMoq.Object, serviceMoq.Object);

    // Act
    var actionResult = await controller.Unregister(payload);

    // Assert
    var okObjectResult = actionResult as ObjectResult;
    Assert.That(okObjectResult, Is.Not.Null);
    okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    okObjectResult.Value.Should().BeEquivalentTo(result);
  }

  [Test]
  public async Task Unregister_ShouldReturn_Status500InternalServerError_OnError()
  {
    // Arrange
    int payload = 1;
    var errorMessage = "Expected exception from moq";
    var loggerMoq = new Mock<ILogger<ApplicationController>>();
    var serviceMoq = new Mock<IDefaultService<GateApplicationMetadataModel>>();

    serviceMoq.Setup(service => service.RemoveAsync(It.IsAny<int>()))
               .ThrowsAsync(new Exception(errorMessage));
    var controller = new ApplicationController(loggerMoq.Object, serviceMoq.Object);

    // Act
    var actionResult = controller.Unregister(payload);

    // Assert
    var errorObjectResult = await actionResult as ObjectResult;
    Assert.That(errorObjectResult, Is.Not.Null);
    errorObjectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    var problemDetail = errorObjectResult.Value as ProblemDetails;
    Assert.That(problemDetail, Is.Not.Null);
    problemDetail.Detail.Should().Contain(errorMessage);
  }
}
