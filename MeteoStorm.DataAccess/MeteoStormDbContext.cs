using MeteoStorm.DataAccess.Models;
using MeteoStorm.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Options;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MeteoStormDb;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(CityConfiguration).Assembly);
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeteoDataEntryConfiguration).Assembly);
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<MeteoDataEntry> MeteoDataEntries { get; set; }
    public DbSet<User> Users { get; set; }
  }
}