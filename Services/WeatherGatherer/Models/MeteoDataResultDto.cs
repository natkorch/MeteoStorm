namespace Services.WeatherGatherer.Models
{
  public class MeteoDataResultDto
  {
    /// <summary>
    /// Температура в указанном месте на момент замера
    /// </summary>
    public double Temperature { get; set; }

    public string ErrorMessage { get; set; }
  }
}
