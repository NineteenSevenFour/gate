using Microsoft.EntityFrameworkCore;

using NineteenSevenFour.Gatehub.Domain.Entities;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Context
{
  /// <summary>
  /// SQLite DBContext for GATEHUB Api
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class SqliteDbContext : DbContext
  {
    /// <summary>
    /// Gets or sets the gate application metadata.
    /// </summary>
    /// <value>
    /// The gate application metadata.
    /// </value>
    public DbSet<GateApplicationMetadataEntity>? GateApplicationMetadata { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqliteDbContext"/> class.
    /// </summary>
    /// <remarks>
    /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information and examples.
    /// </remarks>
    public SqliteDbContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqliteDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
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
