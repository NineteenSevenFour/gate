using Microsoft.EntityFrameworkCore;

using NineteenSevenFour.Gatehub.Domain.Entities;

using System.Reflection;

namespace NineteenSevenFour.Gatehub.Data.sqlite.Context
{
  /// <summary>
  /// SQLite DBContext for GATEHUB Api
  /// </summary>
  public class SqliteDbContext : DbContext
  {
    public DbSet<GateApplicationMetadataEntity>? GateApplicationMetadata { get; set; }

    public SqliteDbContext() { }

    public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Scan the assembly for all IEntityTypeConfiguration<> to configure Entities+Relations
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
      // Define global conversion if any.
    }
  }
}
