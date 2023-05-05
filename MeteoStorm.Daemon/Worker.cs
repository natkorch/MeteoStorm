using MeteoStorm.Daemon.Jobs;
using MeteoStorm.Daemon.Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using Serilog;

namespace MeteoStorm.Daemon
{
  public class Worker : BackgroundService
  {
    private IServiceProvider _serviceProvider;
    private App _app;

    public void ConfigureServices(ServiceCollection services)
    {
      services.AddLogging(config => config.AddSerilog());
      services.AddSingleton<ISchedulerFactory>(x => new StdSchedulerFactory());
      services.AddSingleton<IJobFactory, InjectionJobFactory>();
      services.AddSingleton<HeartBeatJob>();
      services.AddTransient<App>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var services = new ServiceCollection();
      ConfigureServices(services);
      _serviceProvider = services.BuildServiceProvider();
      _app = _serviceProvider.GetRequiredService<App>();
      await _app.Execute(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
      await _app.Stop();
      Log.CloseAndFlush();
      if (_serviceProvider is IDisposable)
      {
        ((IDisposable)_serviceProvider).Dispose();
      }
    }
  }
}