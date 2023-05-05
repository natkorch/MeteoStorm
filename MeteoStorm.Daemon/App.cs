using MeteoStorm.Daemon.Jobs;
using MeteoStorm.Daemon.Quartz;
using Quartz;
using Quartz.Spi;

namespace MeteoStorm.Daemon
{
  public class App
  {
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private IScheduler _scheduler;

    public App(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
    {
      _schedulerFactory = schedulerFactory;
      _jobFactory = jobFactory;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
      _scheduler.JobFactory = _jobFactory;

      await _scheduler.ScheduleSimple<HeartBeatJob>(x => x
        .WithIntervalInSeconds(3)
        .RepeatForever(), cancellationToken);

      await _scheduler.Start(cancellationToken);
    }

    public async Task Stop()
    {
      await _scheduler.Shutdown();
    }
  }
}
