using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NineteenSevenFour.Gatehub.Data.sqlite.Context
{
  public static class SqliteDbContextExtension
  {
    public static string GetConnectionString(
      string filename = "gatehub.db") => $"Data Source={filename};";

    public static IServiceCollection AddSqliteDbFactory(this IServiceCollection services, ConfigurationManager configuration)
    {
      var filename = configuration["Database:Sqlite:Filename"] ?? "gatehub.db";

      if (!bool.TryParse(configuration["EnableDetailedErrors"], out bool enableDetailedErrors))
      {
        enableDetailedErrors = false;
      }
      if (!bool.TryParse(configuration["EnableSensitiveDataLogging"], out bool enableSensitiveDataLogging))
      {
        enableSensitiveDataLogging = false;
      }

      services.AddDbContextFactory<SqliteDbContext>(options =>
      {
        options
          .UseSqlite(
              GetConnectionString(filename),
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
        ;
      });

      services.AddScoped(
        sp => sp.GetRequiredService<IDbContextFactory<SqliteDbContext>>()
                .CreateDbContext()
      );

      return services;
    }

    public static Task<SqliteDbContext> Create(
      string filename,
      bool enableDetailedErrors,
      bool enableSensitiveDataLogging)
    {
      var connectionString = GetConnectionString(filename);

      Console.WriteLine(connectionString);

      var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>()
          .UseSqlite(
              GetConnectionString(filename),
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

      return Task.FromResult(new SqliteDbContext(optionsBuilder.Options));
    }
  }
}
