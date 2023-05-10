using RestSharp;
using Services.WeatherGatherer.Models;
using Serilog;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Globalization;

namespace Services.WeatherGatherer
{
  public class WttrClient : IWeatherClient
  {
    private const string BaseUrl = "https://wttr.in/";
    private readonly RestClient _client;
    private static readonly ILogger _logger = Log.ForContext<WttrClient>();

    public WttrClient()
    {
      _client = new RestClient(BaseUrl);
    }

    public static string WeatherServiceName => "wttr";

    public void Dispose()
    {
      _client?.Dispose();
    }

    public async Task<MeteoDataResultDto> GetMeteoData(MeteoDataRequestDto meteoRequest)
    {
      var urlTemplate = "{0}?format=%t";
      var url = string.Format(urlTemplate, meteoRequest.EnglishName);
      var request = new RestRequest(url, Method.Get);
      var response = await ExecuteAsync(request);

      if (!response.IsSuccessful)
        return new MeteoDataResultDto { ErrorMessage = response.ErrorMessage };

      var result = ParseTemperature(response.Content);
      if (!string.IsNullOrEmpty(result.ErrorMessage))
        Log.Error(result.ErrorMessage);

      return result;
    }

    private async Task<RestResponse> ExecuteAsync(RestRequest request)
    {
      var stopWatch = new Stopwatch();
      stopWatch.Start();
      var response = await _client.ExecuteAsync(request);
      stopWatch.Stop();
      if (!response.IsSuccessful) 
      {
        Log.Error("Error trying to obtain data via {WeatherServiceName}: {ErrorMessage}", 
          WeatherServiceName, response.ErrorMessage);
      }
      LogRequest(_client, request, response, stopWatch.ElapsedMilliseconds);
      return response;
    }

    /// <summary>Logging request and response</summary>
    /// <remarks>
    /// From https://stackoverflow.com/questions/15683858/restsharp-print-raw-request-and-response-headers
    /// </remarks>
    /// <param name="restClient"></param>
    /// <param name="request"></param>
    /// <param name="response"></param>
    /// <param name="durationMs"></param>
    private void LogRequest(IRestClient restClient, RestRequest request, RestResponse response, long durationMs)
    {
      var requestToLog = new
      {
        resource = request.Resource,
        // Parameters are custom anonymous objects in order to have the parameter type as a nice string
        // otherwise it will just show the enum value
        parameters = request.Parameters.Select(parameter => new
        {
          name = parameter.Name,
          value = parameter.Value,
          type = parameter.Type.ToString()
        }),
        // ToString() here to have the method as a nice string otherwise it will just show the enum value
        method = request.Method.ToString(),
        // This will generate the actual Uri used in the request
        uri = restClient.BuildUri(request)
      };

      var responseToLog = new
      {
        statusCode = response.StatusCode,
        content = response.Content,
        headers = response.Headers,
        // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
        responseUri = response.ResponseUri,
        errorMessage = response.ErrorMessage
      };

      _logger
        .Information(
        $"Request completed in {durationMs} ms, " +
        $"Request: {JsonConvert.SerializeObject(requestToLog)}, " +
        $"Response: {JsonConvert.SerializeObject(responseToLog)}");
    }

    private static MeteoDataResultDto ParseTemperature(string temperatureString)
    {
      var result = new MeteoDataResultDto();
      var temperature = 0D;
      var valueWithoutUnit = temperatureString.Replace("°C", "").Trim();

      if (double.TryParse(valueWithoutUnit, CultureInfo.InvariantCulture, out temperature))
        result.Temperature = temperature;
      else
        result.ErrorMessage = $"Can't parse [{temperatureString}] into double";

      return result;
    }
  }
}
