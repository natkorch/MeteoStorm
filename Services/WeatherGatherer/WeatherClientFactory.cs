namespace Services.WeatherGatherer
{
  public class WeatherClientFactory
  {
    public static IWeatherClient ChooseWeatherClient(string weatherServiceName)
    {
      switch (weatherServiceName)
      {
        case string w when w == WttrClient.WeatherServiceName:
          return new WttrClient();
        default:
          throw new Exception($"Unknown Weather Service: {weatherServiceName}");
      }
    }
  }
}