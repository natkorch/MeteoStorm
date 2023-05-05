using Serilog;

namespace MeteoStorm.Daemon
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Environment.CurrentDirectory = AppContext.BaseDirectory;
      InitLogger();

      try
      {
        Log.Information("�������� ������");
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        Log.Error(ex, "������ ��� ������� �������");
      }
      finally
      {
        Log.Information("������ ����������");
        Log.CloseAndFlush();
      }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .ConfigureServices((hostContext, services) =>
        {
          services.AddHostedService<Worker>();
        }).UseSerilog();

    private static void InitLogger()
    {
      Log.Logger = new LoggerConfiguration()
       .WriteTo.Logger(lg =>
         lg
           .MinimumLevel.Debug()
           .WriteTo.Console()
           .WriteTo.File(@"Logs\trace-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
       )
       .WriteTo.Logger(lg =>
          lg
          .MinimumLevel.Error()
          .WriteTo.Console()
          .WriteTo.File(@"Logs\error-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
      )
      .CreateLogger();
    }
  }
}