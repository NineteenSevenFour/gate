using Microsoft.AspNetCore.Mvc;

using NineteenSevenFour.Gatehub.Domain.Exceptions;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
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
  private readonly IApplicationService applicationService;

  /// <summary>
  /// Constructor of the application endpoint controller.
  /// </summary>
  /// <param name="logger">The application logging service</param>
  /// <param name="applicationService">The GATE application management service</param>
  public ApplicationController(ILogger<ApplicationController> logger, IApplicationService applicationService)
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
  [ProducesResponseType(typeof(GateApplicationMetadata[]), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult List()
  {
    try
    {
      return Ok(this.applicationService.GetAll());
    }
    catch (EntityNotFoundException notFoundEx)
    {
      return NotFound(notFoundEx.Message);
    }
    catch (Exception ex)
    {
      this.logger.LogError(ex.Message, ex.InnerException);

      string message = ex.Message;
      if (!string.IsNullOrWhiteSpace(ex?.InnerException?.Message))
      {
        message += $" - Inner exception: {ex.InnerException.Message}";
      }

      return Problem(detail: message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }
}
