using Microsoft.AspNetCore.Mvc;

using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.Domain.Models;

namespace NineteenSevenFour.Gatehub.Controllers;

/// <summary>
/// Application endpoint to un/register or list GATE application
/// </summary>
[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
  private readonly ILogger<ApplicationController> logger;
  private readonly IDefaultService<GateApplicationMetadataModel> applicationService;

  /// <summary>
  /// Constructor of the application endpoint controller.
  /// </summary>
  /// <param name="logger">The application logging service</param>
  /// <param name="applicationService">The GATE application management service</param>
  public ApplicationController(ILogger<ApplicationController> logger, IDefaultService<GateApplicationMetadataModel> applicationService)
  {
    this.logger = logger;
    this.applicationService = applicationService;
  }

  /// <summary>
  /// Endpoint to retrieve the registered GATE applications.
  /// </summary>
  /// <returns>A list of GATE applictions</returns>
  /// <response code="200">Application list retrieved</response>
  /// <response code="500">Unhandled error occured internaly.</response>
  [HttpGet()]
  [Route("List")]
  [Produces("application/json")]
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

  /// <summary>
  /// Endpoint to register a GATE application
  /// </summary>
  /// <response code="200">Application registered</response>
  /// <response code="500">Unhandled error occured internaly.</response>
  [HttpPost()]
  [Route("Register")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(GateApplicationMetadataModel), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Register([FromBody(EmptyBodyBehavior = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior.Disallow)] GateApplicationMetadataRegistrationModel application)
  {
    try
    {
      return Ok(await this.applicationService.AddAsync(new GateApplicationMetadataModel(application)));
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "{message}", ex.Message);
      return Problem(detail: string.Format("{0}", ex.Message), statusCode: StatusCodes.Status500InternalServerError);
    }
  }

  /// <summary>
  /// Endpoint to unregister a GATE application
  /// </summary>
  /// <param name="id" example="1">The unique identifier of the GATE application to unregister</param>
  /// <response code="200">Application unregistered</response>
  /// <response code="500">Unhandled error occured internaly.</response>
  [HttpPost()]
  [Route("{id}/Unregister")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Unregister(int id)
  {
    try
    {
      return Ok(await this.applicationService.RemoveAsync(id));
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "{message}", ex.Message);
      return Problem(detail: string.Format("{0}", ex.Message), statusCode: StatusCodes.Status500InternalServerError);
    }
  }
}
