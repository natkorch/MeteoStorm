using Services.WeatherGatherer.Models;

namespace Services.WeatherGatherer
{
  public interface IWeatherClient : IDisposable
  {
    const string WeatherServiceName = "Unknown";
    Task<MeteoDataResultDto> GetMeteoData(MeteoDataRequestDto message);
  }
}
