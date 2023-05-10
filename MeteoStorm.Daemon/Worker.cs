using MeteoStorm.Daemon.Jobs;
using Quartz;
using Serilog;
using MeteoStorm.DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.WeatherGatherer;
using Microsoft.Extensions.Options;

namespace MeteoStorm.Daemon
{
  public class Worker : BackgroundService
  {
    private IServiceProvider _serviceProvider;
    private readonly IConfiguration _config;
    private IScheduler _scheduler;

    public Worker(IConfiguration config)
    {
      _config = config;
    }

    public void ConfigureServices(ServiceCollection services)
    {
      services.AddLogging(config => config.AddSerilog());

      services.AddDbContext<MeteoStormDbContext>(options =>
      {
        options.UseSqlServer(_config.GetConnectionString("MeteoStormDb"));
      });

      services.Configure<AppOptions>(_config.GetSection("AppOptions"));

      services.AddSingleton<IWeatherClient>(sp =>
      {
        var options = sp.GetRequiredService<IOptions<AppOptions>>().Value;
        return WeatherClientFactory.ChooseWeatherClient(options.WeatherService);
      });

      services.AddQuartz(q =>
      {
        q.UseMicrosoftDependencyInjectionJobFactory();
        JobScheduler.ScheduleJobs(q);
      });
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var services = new ServiceCollection();
      ConfigureServices(services);
      _serviceProvider = services.BuildServiceProvider();
      _scheduler = await _serviceProvider.GetRequiredService<ISchedulerFactory>().GetScheduler(stoppingToken);

      var reportFolder = _serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value.ReportFolder;
      if (!Directory.Exists(reportFolder))
        Directory.CreateDirectory(reportFolder);

      await _scheduler.Start(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
      await _scheduler.Shutdown(waitForJobsToComplete: true);
      if (_serviceProvider is IDisposable)
      {
        ((IDisposable)_serviceProvider).Dispose();
      }
    }
  }
}