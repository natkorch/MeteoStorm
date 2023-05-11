namespace Services.WeatherGatherer.Models
{
  /// <summary>
  /// Represents a data transfer object for making a request 
  /// to a weather service API for weather information at a specific location
  /// </summary>
  public class MeteoDataRequestDto
  {
    /// <summary>
    /// The name of the city in English
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// The latitude, expressed in decimal degrees
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// The longitude, expressed in decimal degrees
    /// </summary>
    public decimal Longitude { get; set; }
  }
}
