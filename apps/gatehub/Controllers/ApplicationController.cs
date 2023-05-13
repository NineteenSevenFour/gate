using Microsoft.AspNetCore.Mvc;
using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces.Services;
using NineteenSevenFour.Gatehub.Domain.Models;

namespace NineteenSevenFour.Gatehub.Controllers;

/// <summary>
/// Application endpoint to un/register or list GATE application
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ApplicationController : ControllerBase
{
  private readonly ILogger<ApplicationController> logger;
  private readonly IService<GateApplicationMetadataModel, GateApplicationMetadataEntity> applicationService;

  /// <summary>
  /// Constructor of the application endpoint controller.
  /// </summary>
  /// <param name="logger">The application logging service</param>
  /// <param name="applicationService">The GATE application management service</param>
  public ApplicationController(ILogger<ApplicationController> logger, IService<GateApplicationMetadataModel, GateApplicationMetadataEntity> applicationService)
  {
    this.logger = logger;
    this.applicationService = applicationService;
  }

  /// <summary>
  /// Endpoint to retrieve the registered GATE applications.
  /// </summary>
  /// <returns>A list of GATE applictions</returns>
  [HttpGet()]
  [Route("List")]
  [ProducesResponseType(typeof(GateApplicationMetadataModel[]), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult List()
  {
    try
    {
      return Ok(this.applicationService.GetAll());
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "{message}", ex.Message);
      return Problem(detail: string.Format("{0}", ex.Message), statusCode: StatusCodes.Status500InternalServerError);
    }
  }
}
