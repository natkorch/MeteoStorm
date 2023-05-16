using MeteoStorm.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MeteoStorm.DataAccess.Configuration
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.HasKey(c => c.Id);
      builder.Property(x => x.Login).IsRequired().HasMaxLength(32);
      builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(256);
      builder.Property(x => x.Role).IsRequired().HasMaxLength(32);
      builder.Property(x => x.IsActive).IsRequired();
      builder.Property(x => x.FirstName).HasMaxLength(64);
      builder.Property(x => x.Patronymic).HasMaxLength(64);
      builder.Property(x => x.LastName).HasMaxLength(64);
      builder.HasOne(c => c.City);
    }
  }
}
