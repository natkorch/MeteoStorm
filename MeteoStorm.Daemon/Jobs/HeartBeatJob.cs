using MeteoStorm.DataAccess;
using MeteoStorm.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace MeteoStorm.Daemon.Jobs
{
  [DisallowConcurrentExecution]
  public class HeartBeatJob : IJob
  {
    private readonly ILogger<HeartBeatJob> _logger;
    private readonly MeteoStormDbContext _dbContext;

    public HeartBeatJob(ILogger<HeartBeatJob> logger, MeteoStormDbContext dbContext)
    {
      _logger = logger;
      _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      _logger.LogInformation("HeartBeatJob STARTED");
      _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now.ToString());

      var cities = await _dbContext.Cities.ToListAsync();

      var randomIndex = new Random().Next(cities.Count);
      var randomCity = cities[randomIndex];

      var currentUtcTime = new DateTimeOffset(DateTime.UtcNow, TimeSpan.FromMinutes(0));
      var cityTime = currentUtcTime.ToOffset(TimeSpan.FromMinutes(randomCity.TimeZoneOffset));

      _logger.LogInformation("Локальное время в городе {CityName}: {DateTime}", 
        randomCity.RussianName, cityTime.DateTime.ToString());

      _logger.LogInformation("HeartBeatJob ENDED");
    }
  }
}