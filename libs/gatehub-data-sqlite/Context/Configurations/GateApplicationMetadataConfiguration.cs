using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NineteenSevenFour.Gatehub.Domain.Entities;

namespace NineteenSevenFour.Gatehub.Data.sqlite.Context.Configurations
{
  public sealed class GateApplicationMetadataConfiguration : IEntityTypeConfiguration<GateApplicationMetadataEntity>
  {
    public void Configure(EntityTypeBuilder<GateApplicationMetadataEntity> entityBuilder)
    {
      entityBuilder
        .ToTable("applicationmetadata")
        .HasKey(e => e.Id);

      entityBuilder
        .HasIndex(e => e.Id, "applicationmetadata_index");

      entityBuilder
        .Property(e => e.Id)
        .ValueGeneratedOnAdd()
        .HasColumnType("INTEGER")
        .HasColumnName("id");

      entityBuilder
        .Property(e => e.Description)
        .IsRequired()
        .HasColumnName("description")
        .HasColumnType("TEXT");

      entityBuilder
        .Property(e => e.Icon)
        .IsRequired()
        .HasColumnName("icon")
        .HasColumnType("TEXT");
    }
  }
}
