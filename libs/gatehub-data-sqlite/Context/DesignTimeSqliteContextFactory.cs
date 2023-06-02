using Microsoft.EntityFrameworkCore.Design;

using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Context
{
  [ExcludeFromCodeCoverage]
  public class DesignTimeSqliteContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
  {
    public SqliteDbContext CreateDbContext(string[] args)
    {
      var rootCommand = new RootCommand("Create a SqliteDBContext");

      var filename = new Option<string>(new[] { "--filename", "-f" }, "SQLite DB filename.");
      rootCommand.AddOption(filename);

      var enableDetailedErrors = new Option<bool>("--enableDetailedErrors", () => false, "Enable/Disable the logging of detailled errors.");
      rootCommand.AddOption(enableDetailedErrors);

      var enableSensitiveDataLogging = new Option<bool>("--enableSensitiveDataLogging", () => false, "Enable/Disable the logging of sensitive data.");
      rootCommand.AddOption(enableSensitiveDataLogging);

      SqliteDbContext? context = null;

      rootCommand.SetHandler(
          async (filename, enableDetailedErrors, enableSensitiveDataLogging) =>
          {
            context = await SqliteDbContextExtension.Create(
              filename,
              enableDetailedErrors,
              enableSensitiveDataLogging
            );
          },
          filename, enableDetailedErrors, enableSensitiveDataLogging);

      rootCommand.Invoke(args);

#pragma warning disable CS8603 // Possible null reference return.
      return context;
#pragma warning restore CS8603 // Possible null reference return.
    }
  }
}
