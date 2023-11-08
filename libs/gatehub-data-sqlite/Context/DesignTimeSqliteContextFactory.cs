using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Context
{
  /// <summary>
  /// Facory for design time generation of SQLite DBContext
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class DesignTimeSqliteContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
  {
    /// <summary>
    /// SQLite DBContext factory entry point
    /// </summary>
    /// <param name="args">Arguments necessary to create the SQLite DBContext</param>
    /// <returns></returns>
    public SqliteDbContext CreateDbContext(string[] args)
    {
      var loggerFactory = new LoggerFactory();
      var logger = loggerFactory.CreateLogger("Design time Sqlite DBContext factory");

      var rootCommand = new RootCommand("Create a SqliteDBContext");

      var filename = new Option<string>(new[] { "--filename", "-f" }, "SQLite DB filename.");
      Console.WriteLine($"DesignTime Sqlite DBContext factory: filename={filename}");
      rootCommand.AddOption(filename);

      var enableDetailedErrors = new Option<bool>("--enableDetailedErrors", () => false, "Enable/Disable the logging of detailled errors.");
      Console.WriteLine($"DesignTime Sqlite DBContext factory: enableDetailedErrors={enableDetailedErrors}");
      rootCommand.AddOption(enableDetailedErrors);

      var enableSensitiveDataLogging = new Option<bool>("--enableSensitiveDataLogging", () => false, "Enable/Disable the logging of sensitive data.");
      Console.WriteLine($"DesignTime Sqlite DBContext factory: enableSensitiveDataLogging={enableSensitiveDataLogging}");
      rootCommand.AddOption(enableSensitiveDataLogging);

      SqliteDbContext? context = null;

      rootCommand.SetHandler(
          (filename, enableDetailedErrors, enableSensitiveDataLogging) =>
          {
            var connectionString = SqliteDbContextExtension.GetConnectionString(filename);

            Console.WriteLine($"DesignTime Sqlite DBContext factory: create DBContext [{connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>().ConfigureDbContext(logger, filename, enableDetailedErrors, enableSensitiveDataLogging);

            context = new SqliteDbContext(((DbContextOptionsBuilder<SqliteDbContext>)optionsBuilder).Options);
          },
          filename, enableDetailedErrors, enableSensitiveDataLogging);

      rootCommand.Invoke(args);

#pragma warning disable CS8603 // Possible null reference return.
      return context;
#pragma warning restore CS8603 // Possible null reference return.
    }
  }
}
