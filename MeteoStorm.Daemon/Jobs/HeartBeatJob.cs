using Quartz;

namespace MeteoStorm.Daemon.Jobs
{
  [DisallowConcurrentExecution]
  public class HeartBeatJob : IJob
  {
    private readonly ILogger<HeartBeatJob> _logger;

    public HeartBeatJob(ILogger<HeartBeatJob> logger)
    {
      _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      _logger.LogInformation("HeartBeatJob STARTED");
      _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now.ToString());
      _logger.LogInformation("HeartBeatJob ENDED");
    }
  }
}