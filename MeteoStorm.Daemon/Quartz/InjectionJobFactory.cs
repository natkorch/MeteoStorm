using Quartz;
using Quartz.Spi;

namespace MeteoStorm.Daemon.Quartz
{
  public class InjectionJobFactory : IJobFactory
  {
    private readonly IServiceProvider _serviceProvider;
    public InjectionJobFactory(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
      return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job) { }
  }
}
