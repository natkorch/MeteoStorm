using Quartz;

namespace MeteoStorm.Daemon.Jobs
{
  public class JobScheduler
  {
    public static void ScheduleJobs(IServiceCollectionQuartzConfigurator q)
    {
      q.ScheduleJob<HeartBeatJob>(trigger => trigger
        .WithIdentity("HeartBeatJob")
        .StartNow()
        .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
        .WithDescription("Checking if service is on (Logging local time of a random city)")
      );

      q.ScheduleJob<GatherMeteoDataJob>(trigger => trigger
        .WithIdentity("GatherMeteoDataJob")
        .StartNow()
        .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
        .WithDescription("Gathering meteo data")
      );

      q.ScheduleJob<TemperatureReportJob>(trigger => trigger
        .WithIdentity("TemperatureReportJob")
        .StartNow()
        .WithCronSchedule("0 0 12 * * ?")
        .WithDescription("Analyzing daily meteo data and storing in report files")
      );
    }
  }
}
