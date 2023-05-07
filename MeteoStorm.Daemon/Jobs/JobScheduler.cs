using Quartz;

namespace MeteoStorm.Daemon.Jobs
{
  public class JobScheduler
  {
    public static void ScheduleJobs(IServiceCollectionQuartzConfigurator q)
    {
      //q.ScheduleJob<HeartBeatJob>(trigger => trigger
      //  .WithIdentity("HeartBeatJob")
      //  .StartNow()
      //  .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
      //  .WithDescription("Checking if service is on")
      //);

      q.ScheduleJob<GatherMeteoDataJob>(trigger => trigger
        .WithIdentity("GatherMeteoDataJob")
        .StartNow()
        .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
        .WithDescription("Gathering meteo data")
      );
    }
  }
}
