using MeteoStorm.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeteoStorm.DataAccess.Configuration
{
  public class CityConfiguration : IEntityTypeConfiguration<City>
  {
    public void Configure(EntityTypeBuilder<City> builder)
    {
      builder.HasKey(c => c.Id);
      builder.Property(x => x.RussianName).HasMaxLength(128).IsRequired();
      builder.Property(x => x.EnglishName).HasMaxLength(128);
      builder.Property(c => c.Latitude).IsRequired().HasColumnType("decimal(9,6)");
      builder.Property(c => c.Longitude).IsRequired().HasColumnType("decimal(9,6)");
      builder.Property(x => x.TimeZoneOffset).IsRequired();
      builder.Property(x => x.GatherMeteoData).IsRequired();
    }
  }
}