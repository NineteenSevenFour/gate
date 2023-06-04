using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Tests.Controllers;

[ExcludeFromCodeCoverage]
public abstract class MockHostingEnvironment : IHostEnvironment
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  protected MockHostingEnvironment(string environmentName, string applicationName)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  {
    EnvironmentName = environmentName ?? throw new ArgumentNullException(nameof(environmentName));
    ApplicationName = applicationName ?? throw new ArgumentNullException(nameof(applicationName));
  }

  public string EnvironmentName { get; set; }

  public string ApplicationName { get; set; }

  public string ContentRootPath { get; set; }

  public IFileProvider ContentRootFileProvider { get; set; }
}

[ExcludeFromCodeCoverage]
public class MockProdHostingEnvironment : MockHostingEnvironment
{
  public MockProdHostingEnvironment() : this("Production", "My Prod App") { }

  public MockProdHostingEnvironment(string environmentName, string applicationName) : base(environmentName, applicationName)
  {
  }
}

[ExcludeFromCodeCoverage]
public class MockDevHostingEnvironment : MockHostingEnvironment
{
  public MockDevHostingEnvironment() : this("Development", "My Dev App") { }

  public MockDevHostingEnvironment(string environmentName, string applicationName) : base(environmentName, applicationName)
  {
  }
}
