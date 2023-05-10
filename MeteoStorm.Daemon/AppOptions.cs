namespace MeteoStorm.Daemon
{
  /// <summary>
  /// Application options that can be configured by the user
  /// </summary>
  public class AppOptions
  {
    /// <summary>
    /// The name of the weather service to use when gathering weather data
    /// </summary>
    public string WeatherService { get; set; }

    /// <summary>
    /// The folder name where reports will be saved
    /// </summary>
    public string ReportFolder { get; set; }
  }
}
