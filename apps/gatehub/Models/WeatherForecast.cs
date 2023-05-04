namespace NineteenSevenFour.Gatehub.Models;

/// <summary>
/// Weatherr forecast model
/// </summary>
public class WeatherForecast
{
  /// <summary>
  /// Date of forecast
  /// </summary>
  public string? Date { get; set; }

  /// <summary>
  /// Expected temperature in Celcius
  /// </summary>
  public int TemperatureC { get; set; }


  /// <summary>
  /// Expected temperature in Farenheit
  /// </summary>
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

  /// <summary>
  /// Forecast summary
  /// </summary>
  public string? Summary { get; set; }
}
