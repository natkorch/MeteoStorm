using MeteoStorm.DataAccess;
using MeteoStorm.DataAccess.Constants;
using MeteoStorm.DataAccess.Models;
using MeteoStorm.InfoHub.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Services;
using ILogger = Serilog.ILogger;

namespace MeteoStorm.InfoHub
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .Build();

      var logger = InitLogger(configuration);
      builder.Logging.ClearProviders();
      builder.Host.UseSerilog(logger);

      builder.Services.AddHttpContextAccessor();

      var connectionString = configuration.GetConnectionString("MeteoStormDb");
      builder.Services.AddDbContext<MeteoStormDbContext>(options =>
          options.UseSqlServer(connectionString));

      builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

      builder.Services.AddControllersWithViews();

      builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
            options.LoginPath = "/Access/Login";
            options.AccessDeniedPath = "/Access/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
          });

      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy(Policies.AdminOnly, policy => policy.RequireRole(AppRoles.Admin));
        options.AddPolicy(Policies.Employees, policy => policy.RequireRole(AppRoles.Admin, AppRoles.Operator));
        options.AddPolicy(Policies.Clients, policy => policy.RequireRole(AppRoles.Client));
      });

      builder.Services.AddScoped<PasswordHasher<User>>();
      builder.Services.AddScoped<UserService>();

      var app = builder.Build();

      if (app.Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Access}/{action=Login}/{id?}");

      app.Run();
    }

    public static ILogger InitLogger(IConfiguration configuration)
    {
      Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .Enrich.FromLogContext()
          .WriteTo.Logger(lg =>
              lg
              .MinimumLevel.Debug()
              .WriteTo.Console()
              .WriteTo.File(@"Logs\trace-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
           )
          .WriteTo.Logger(lg =>
              lg
              .MinimumLevel.Error()
              .WriteTo.File(@"Logs\error-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
          )
          .CreateLogger();

      return Log.Logger;
    }
  }
}