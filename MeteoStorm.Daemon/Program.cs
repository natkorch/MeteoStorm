using Serilog;
using Serilog.Filters;
using Services.WeatherGatherer;

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
        Log.Information("Запускаю сервис");
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Ошибка при запуске сервиса");
      }
      finally
      {
        Log.Information("Сервис остановлен");
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
           .Filter.ByExcluding(Matching.FromSource<WttrClient>())
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .WriteTo.File(@"Logs\trace-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
       )
       .WriteTo.Logger(lg =>
           lg.Filter.ByIncludingOnly(Matching.FromSource<WttrClient>())
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .WriteTo.File(@"Logs\wttr-client-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
         )
       .WriteTo.Logger(lg =>
          lg
          .MinimumLevel.Error()
          .Enrich.FromLogContext()
          .WriteTo.File(@"Logs\error-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
      )
      .CreateLogger();
    }
  }
}