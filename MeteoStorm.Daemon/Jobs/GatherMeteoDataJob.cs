using MeteoStorm.DataAccess;
using MeteoStorm.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Services.WeatherGatherer;
using Services.WeatherGatherer.Models;

namespace MeteoStorm.Daemon.Jobs
{
  [DisallowConcurrentExecution]
  public class GatherMeteoDataJob : IJob
  {
    private readonly ILogger<HeartBeatJob> _logger;
    private readonly MeteoStormDbContext _dbContext;
    private readonly IWeatherClient _weatherClient;

    public GatherMeteoDataJob(ILogger<HeartBeatJob> logger, MeteoStormDbContext dbContext,
      IWeatherClient weatherClient
      )
    {
      _logger = logger;
      _dbContext = dbContext;
      _weatherClient = weatherClient;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      _logger.LogInformation("GatherMeteoDataJob STARTED");

      var cities = await _dbContext.Cities
        .Where(c => c.GatherMeteoData)
        .ToListAsync();

      var records = new List<MeteoDataEntry>();
      foreach (var city in cities) 
      {
        var temperature = await _weatherClient.GetMeteoData(new MeteoDataRequestDto
        {
          EnglishName = city.EnglishName,
          Latitude = city.Latitude,
          Longitude = city.Longitude
        });
        if (string.IsNullOrEmpty(temperature.ErrorMessage))
        {
          var now = DateTimeOffset.Now;
          var record = MeteoDataEntry.Create(city, DateTimeOffset.Now, temperature.Temperature);
          records.Add(record);
        }
      }

      await _dbContext.MeteoDataEntries.AddRangeAsync(records);
      await _dbContext.SaveChangesAsync();
      _logger.LogInformation("{RecordCount} new meteo data entries have been added", records.Count);
      _logger.LogInformation("GatherMeteoDataJob ENDED");
    }
  }
}