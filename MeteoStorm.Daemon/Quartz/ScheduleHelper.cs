using Quartz;

namespace MeteoStorm.Daemon.Quartz
{
  public static class ScheduleHelper
  {
    public static async Task ScheduleSimple<T>(this IScheduler scheduler, Action<SimpleScheduleBuilder> scheduleBuilder, CancellationToken cancellationToken,
      string jobGroup = null, string jobId = null) where T : IJob
    {
      var jobIdentity = GetJobIdentity(jobGroup, jobId);
      var job = CreateJob<T>(jobIdentity.jobGroup, jobIdentity.jobId);

      var jobTrigger = TriggerBuilder.Create()
        .WithIdentity($"{jobIdentity.jobId}trigger", jobIdentity.jobGroup)
        .StartNow()
        .WithSimpleSchedule(scheduleBuilder)
        .Build();

      await scheduler.ScheduleJob(job, jobTrigger, cancellationToken);
    }

    public static async Task ScheduleCron<T>(this IScheduler scheduler, string cronExpression, CancellationToken cancellationToken,
      string jobGroup = null, string jobId = null) where T : IJob
    {
      var jobIdentity = GetJobIdentity(jobGroup, jobId);
      var job = CreateJob<T>(jobIdentity.jobGroup, jobIdentity.jobId);

      var jobTrigger = TriggerBuilder.Create()
        .WithIdentity($"{jobIdentity.jobId}trigger", jobIdentity.jobGroup)
        .StartNow()
        .WithCronSchedule(cronExpression)
        .Build();

      await scheduler.ScheduleJob(job, jobTrigger, cancellationToken);
    }

    private static (string jobGroup, string jobId) GetJobIdentity(string jobGroup, string jobId)
    {
      if (jobId == null)
        jobId = "job" + Guid.NewGuid().ToString("N");
      if (jobGroup == null)
        jobGroup = "default";
      return (jobGroup, jobId);
    }

    private static IJobDetail CreateJob<T>(string jobGroup, string jobId) where T : IJob
    {
      var job = JobBuilder.Create<T>()
        .WithIdentity(jobId, jobGroup)
        .Build();
      return job;
    }
  }
}