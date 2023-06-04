using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NineteenSevenFour.Gatehub.Controllers;

/// <summary>
/// Global Error controller
/// </summary>
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
  /// <summary>
  /// Production Error endpoint
  /// </summary>
  /// <returns></returns>
  [Route("/error")]
  public IActionResult HandleError() => Problem();

  /// <summary>
  /// Development Error endpoint
  /// </summary>
  /// <param name="hostEnvironment"></param>
  /// <returns></returns>
  [Route("/error-development")]
  public IActionResult HandleErrorDevelopment(
    [FromServices] IHostEnvironment hostEnvironment)
  {
    if (!hostEnvironment.IsDevelopment())
    {
      return NotFound();
    }

    var exceptionHandlerFeature =
        HttpContext.Features.Get<IExceptionHandlerFeature>()!;

    return Problem(
        detail: exceptionHandlerFeature.Error.StackTrace,
        title: exceptionHandlerFeature.Error.Message);
  }
}
