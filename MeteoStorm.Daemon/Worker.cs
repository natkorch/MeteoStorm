using MeteoStorm.Daemon.Jobs;
using Quartz;
using Serilog;
using MeteoStorm.DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.WeatherGatherer;

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

      services.AddTransient<IWeatherClient>(sp => 
        WeatherClientFactory.ChooseWeatherClient(_config["WeatherService:Name"]));

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
      _scheduler = await _serviceProvider.GetService<ISchedulerFactory>().GetScheduler(stoppingToken);
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