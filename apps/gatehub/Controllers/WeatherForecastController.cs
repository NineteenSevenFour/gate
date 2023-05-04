using Microsoft.AspNetCore.Mvc;
using NineteenSevenFour.Gatehub.Models;

namespace NineteenSevenFour.Gatehub.Controllers;

/// <summary>
/// Weather endpoint 
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class WeatherForecastController : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };

  private readonly ILogger<WeatherForecastController> _logger;

  /// <summary>
  /// Forecast endpoint controller
  /// </summary>
  /// <param name="logger"></param>
  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;
  }

  /// <summary>
  /// Endpoint to retrieve the forecasts
  /// </summary>
  /// <returns></returns>
  [HttpGet(Name = "GetWeatherForecast")]
  public IEnumerable<WeatherForecast> Get()
  {
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)).ToString(),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
  }
}
