using MeteoStorm.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeteoStorm.DataAccess.Configuration
{
  public class MeteoDataEntryConfiguration : IEntityTypeConfiguration<MeteoDataEntry>
  {
    public void Configure(EntityTypeBuilder<MeteoDataEntry> builder)
    {
      builder.HasKey(c => c.Id);
      builder.Property(x => x.City).IsRequired();
      builder.Property(x => x.DateTime).IsRequired();
      builder.Property(x => x.Temperature).IsRequired();
      builder.HasOne(c => c.City).WithMany(x => x.MeteoDataEntries);
    }
  }
}