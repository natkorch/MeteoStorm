namespace Services.WeatherGatherer.Models
{
  public class MeteoDataRequestDto
  {
    /// <summary>
    /// Английское название
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// Широта
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    public decimal Longitude { get; set; }
  }
}
