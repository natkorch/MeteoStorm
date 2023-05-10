using Services.WeatherGatherer.Models;

namespace Services.WeatherGatherer
{
  public interface IWeatherClient : IDisposable
  {
    static string WeatherServiceName { get;  }
    Task<MeteoDataResultDto> GetMeteoData(MeteoDataRequestDto message);
  }
}
