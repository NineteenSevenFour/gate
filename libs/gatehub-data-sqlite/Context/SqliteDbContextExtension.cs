using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Context
{
  /// <summary>
  /// Factory extension for EFCore/SQLite
  /// </summary>
  [ExcludeFromCodeCoverage]
  public static class SqliteDbContextExtension
  {
    /// <summary>
    /// DI service extension to add SQLite db support
    /// </summary>
    /// <param name="services">The DI service colleciton</param>
    /// <param name="configuration">The application configuration manager</param>
    /// <param name="logger">The logger instance</param>
    /// <returns></returns>
    public static IServiceCollection AddSqliteDbFactory(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
      var filename = configuration["Database:Sqlite:Filename"] ?? throw new ApplicationException("Missing DB full path/file name configuration");

      if (!bool.TryParse(configuration["EnableDetailedErrors"], out bool enableDetailedErrors))
      {
        enableDetailedErrors = false;
      }
      if (!bool.TryParse(configuration["EnableSensitiveDataLogging"], out bool enableSensitiveDataLogging))
      {
        enableSensitiveDataLogging = false;
      }

      logger.LogDebug($"Builder: Add SQL DBContext factory [{filename}]");
      services.AddDbContextFactory<SqliteDbContext>(options => options.ConfigureDbContext(logger, filename, enableDetailedErrors, enableSensitiveDataLogging));

      return services;
    }

    /// <summary>
    /// Creat a SQLite DBContext
    /// </summary>
    /// <param name="logger">The logger instance</param>
    /// <param name="filename">SQLite fullpath filename</param>
    /// <param name="enableDetailedErrors">Flag to enable/disable detailled error logging</param>
    /// <param name="enableSensitiveDataLogging">Flag to enable/disable detailled sensitive data logging</param>
    /// <returns></returns>
    public static Task<SqliteDbContext> CreateDbContext(
      ILogger logger,
      string filename,
      bool enableDetailedErrors,
      bool enableSensitiveDataLogging)
    {
      var connectionString = GetConnectionString(filename);

      logger.LogDebug($"SQLite factory: get connection string [{connectionString}");

      var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>().ConfigureDbContext(logger, filename, enableDetailedErrors, enableSensitiveDataLogging);

      return Task.FromResult(new SqliteDbContext(((DbContextOptionsBuilder<SqliteDbContext>)optionsBuilder).Options));
    }

    /// <summary>
    /// Return a SQLite connection string.
    /// </summary>
    /// <param name="filename">Use the given fullpath and name of the SQLite db</param>
    /// <returns></returns>
    public static string GetConnectionString(
      string filename = "/data/db/gatehub.db") => $"Data Source={filename};";

    /// <summary>
    /// Configure the DBContext with SQLite provider
    /// </summary>
    /// <param name="optionsBuilder">DBContext option builder</param>
    /// <param name="logger">The logger instance</param>
    /// <param name="filename">SQLite fullpath filename</param>
    /// <param name="enableDetailedErrors">Flag to enable/disable detailled error logging</param>
    /// <param name="enableSensitiveDataLogging">Flag to enable/disable detailled sensitive data logging</param>
    /// <returns></returns>
    public static DbContextOptionsBuilder ConfigureDbContext(
      this DbContextOptionsBuilder optionsBuilder,
      ILogger logger,
      string filename,
      bool enableDetailedErrors,
      bool enableSensitiveDataLogging)
    {
      var connectionString = GetConnectionString(filename);

      logger.LogDebug($"DbContextOptionsBuilder: use SQLite [{connectionString}]");
      optionsBuilder
          .UseSqlite(
              connectionString,
              builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery))
          .EnableDetailedErrors(enableDetailedErrors)
          .EnableSensitiveDataLogging(enableSensitiveDataLogging)
          .ConfigureWarnings(
              builder => builder.Log(
                  (RelationalEventId.ConnectionOpened, LogLevel.Information),
                  (RelationalEventId.ConnectionClosed, LogLevel.Information),
                  (RelationalEventId.CommandCreated, LogLevel.Information),
                  (RelationalEventId.CommandError, LogLevel.Error)
                  ));
      //// TODO: implement EFCore optimization.
      //.UseModel(SqliteDbContextModel.Instance)

      return optionsBuilder;
    }
  }
}
