using MeteoStorm.DataAccess.Models;
using MeteoStorm.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MeteoStorm.DataAccess
{
  public class MeteoStormDbContext : DbContext
  {
    public MeteoStormDbContext()
    {
    }

    public MeteoStormDbContext(DbContextOptions<MeteoStormDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(CityConfiguration).Assembly);
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeteoDataEntryConfiguration).Assembly);
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<MeteoDataEntry> MeteoDataEntries { get; set; }
  }
}