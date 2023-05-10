using MeteoStorm.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quartz;
using System.Text;

namespace MeteoStorm.Daemon.Jobs
{
  [DisallowConcurrentExecution]
  public class TemperatureReportJob : IJob
  {
    private readonly ILogger<TemperatureReportJob> _logger;
    private readonly MeteoStormDbContext _dbContext;
    private readonly AppOptions _options;

    public TemperatureReportJob(ILogger<TemperatureReportJob> logger, 
      MeteoStormDbContext dbContext, IOptions<AppOptions> options)
    {
      _logger = logger;
      _dbContext = dbContext;
      _options = options.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      try
      {
        _logger.LogInformation("TemperatureReportJob STARTED");

        var yesterday = DateTime.UtcNow.AddDays(-1);
        var dateFrom = new DateTimeOffset(yesterday.Year,
          yesterday.Month,
          yesterday.Day,
          0,
          0,
          0,
          TimeSpan.FromMinutes(0));
        var dateTo = dateFrom.AddDays(1);

        var meteoRecords = await _dbContext.MeteoDataEntries
          .Where(e => e.DateTime >= dateFrom)
          .Where(e => e.DateTime < dateTo)
          .Include(e => e.City)
          .GroupBy(e => e.City)
          .Select(g => new
          {
            CityName = g.Key.RussianName,
            RecordCount = g.Count(),
            MinTemperature = g.Min(e => e.Temperature),
            MaxTemperature = g.Max(e => e.Temperature)
          })
          .ToListAsync();

        var csvPath = Path.Combine(_options.ReportFolder, $"temperatures_{yesterday:yyyy_MM_dd}.csv");

        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
          writer.WriteLine("Город;Количество замеров;Минимальная температура;Максимальная температура");
          foreach (var record in meteoRecords)
          {
            writer.WriteLine($"{record.CityName};" +
              $"{record.RecordCount};" +
              $"{record.MinTemperature};" +
              $"{record.MaxTemperature}");
          }
        }

        _logger.LogInformation($"Meteo data saved to file {csvPath}");
        _logger.LogInformation("TemperatureReportJob ENDED");
      }

      catch (Exception e) 
      {
        _logger.LogError(e, "TemperatureReportJob resulted in error");
      }
    }
  }
}
