using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;

using FluentValidation;
using FluentValidation.AspNetCore;

using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

using Microsoft.OpenApi.Models;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.Domain.Models;
using NineteenSevenFour.Gatehub.Business.Services;
using NineteenSevenFour.Gatehub.Data.Sqlite.Context;
using NineteenSevenFour.Gatehub.Data.Sqlite.Repositories;

using Swashbuckle.AspNetCore.Filters;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization;

using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using System.Collections.Immutable;

var CorsPolicyName = "Gatehub security policy";
var SwaggerDocName = "Gatehub api documentation";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; // allows both to access and to set up the config
var environment = builder.Environment;

if (!Enum.TryParse(configuration["Logging:LogLevel:Default"], out LogLevel logLevel))
{
  logLevel = LogLevel.Information;
}
var loggerFactory = LoggerFactory.Create(b => b.AddConsole().AddFilter("", logLevel));
var logger = loggerFactory.CreateLogger("Gatehub");
logger.LogDebug("Initializing...");

builder.Services.AddSqliteDbFactory(configuration, logger);

logger.LogDebug("Builder: Add auto-mapper");
builder.Services.AddAutoMapper(
    cfg =>
    {
      // Auto register all mapping Profiles
      cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
      // Configure Collection mapping
      cfg.AllowNullCollections = true;
      cfg.AddCollectionMappers();
      cfg.UseEntityFrameworkCoreModel<SqliteDbContext>(builder.Services);
      // configure LINQ query on mapping
      cfg.AddExpressionMapping();
    }
);

// Add services to the container. Having both model and entity as generic types allow
// to use AutoMapper out-of-the-box with generic one-to-one mapping.
// More advanced mapping will requires custom Service, Repository and eventually UnitOfWork

logger.LogDebug("Builder: Add services");
builder.Services.AddScoped<IDefaultService<GateApplicationMetadataModel>, DefaultService<GateApplicationMetadataModel, GateApplicationMetadataEntity>>();

// Add repository to the container.
builder.Services.AddScoped(typeof(IDefaultRepository<>), typeof(DefaultRepository<>));

// Add HealthChecks to the container
logger.LogDebug("Builder: Add health/DBContext checks");
builder.Services.AddHealthChecks()
                .AddDbContextCheck<SqliteDbContext>(name: "Application Database");

//TODO: Review those services
//builder.Services.AddAntiforgery((options) =>
//{
//});

//builder.Services.AddHsts((options) => {
//  options.IncludeSubDomains = true;
//  options.Preload = true;
//  options.MaxAge = new TimeSpan(1,0,0);
//});


logger.LogDebug("Builder: Add cors");
builder.Services.AddCors(options =>
{
  options.AddPolicy(CorsPolicyName, builder =>
  {
    builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});


logger.LogDebug("Builder: Add controllers");
builder.Services
  .AddControllers()
  .AddJsonOptions(o =>
  {
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    o.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals;
  });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
logger.LogDebug("Builder: Add endpoints api explorer");
builder.Services.AddEndpointsApiExplorer();

logger.LogDebug("Builder: Add swaggergen");
builder.Services.AddSwaggerGen(c =>
{
  logger.LogDebug("swagger: Add swaggerdoc");
  c.SwaggerDoc(
    "v1",
    new OpenApiInfo { Title = SwaggerDocName, Version = "v1" }
    );

  // use it if you want to hide Paths and Definitions from OpenApi documentation correctly
  // see https://github.com/unchase/Unchase.Swashbuckle.AspNetCore.Extensions
  c.UseAllOfToExtendReferenceSchemas();

  // see https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters#installation
  logger.LogDebug("swagger: Add api filters");
  c.ExampleFilters();
  c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
  c.OperationFilter<AddResponseHeadersFilter>();

  // Add documentation
  logger.LogDebug("swagger: Add xml documentation");
  Directory
    .GetFiles(AppContext.BaseDirectory, "*.XML", SearchOption.AllDirectories)
    .ToImmutableList()
    .ForEach(f => c.IncludeXmlCommentsWithRemarks(filePath: f));
});

// Add FV, see https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation
logger.LogDebug("Builder: Add fluent validation");
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// Add FV validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// Add FV Rules to swagger
builder.Services.AddFluentValidationRulesToSwagger();

// see https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters#installation
logger.LogDebug("Builder: Add swagger examples");
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

logger.LogDebug("Builder: get application");
WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
//logger.LogDebug("app: set base path");
//app.UsePathBase("/api");
logger.LogDebug("app: map swagger");
app.MapSwagger();
logger.LogDebug("app: use swagger");
app.UseSwagger(c =>
{
  c.RouteTemplate = "doc/{documentname}/swagger.json";
});

if (environment.IsDevelopment())
{
  logger.LogDebug("app: set dev exception endpoint");
  app.UseExceptionHandler("/error-development");

  logger.LogDebug("app: use swagger UI");
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/doc/v1/swagger.json", SwaggerDocName);
    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    c.RoutePrefix = "doc";
  });
}
else
{
  logger.LogDebug("app: set prod exception endpoint");
  app.UseExceptionHandler("/error");
  logger.LogDebug("app: use redoc");
  app.UseReDoc(c =>
  {
    c.DocumentTitle = SwaggerDocName;
    c.SpecUrl = "/doc/v1/swagger.json";
    c.RoutePrefix = "doc";
  });
}

logger.LogDebug("app: set https redirection");
app.UseHttpsRedirection();

logger.LogDebug("app: use cors");
app.UseCors(CorsPolicyName);

logger.LogDebug("app: use authentication");
app.UseAuthentication();

logger.LogDebug("app: use routing");
app.UseRouting();

logger.LogDebug("app: use authorization");
app.UseAuthorization();

logger.LogDebug("app: use static files");
app.UseStaticFiles();

logger.LogDebug("app: map controllers");
app.MapControllers();

logger.LogDebug("app: map health checks");
app.MapHealthChecks("healthchecks");

logger.LogDebug("Running");
app.Run();

/// <summary>
/// Gatehub api entry point
/// </summary>
/// <remarks>
/// Trick the coverage reporting tool to exclude this file
/// </remarks>
[ExcludeFromCodeCoverage(Justification = "API starter program, nothing to test here.")]
public partial class Program { }
