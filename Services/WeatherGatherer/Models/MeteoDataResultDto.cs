namespace Services.WeatherGatherer.Models
{
  /// <summary>
  /// Represents a data transfer object for retrieving weather information from a weather service API
  /// </summary>
  public class MeteoDataResultDto
  {
    /// <summary>
    /// The temperature at the specified location at the time of measurement in degrees Celsius
    /// </summary>
    public double Temperature { get; set; }

    /// <summary>
    /// An error message, if one occurred during the retrieval of the weather data
    /// </summary>
    public string ErrorMessage { get; set; }
  }
}
