using FluentValidation;
using FluentValidation.AspNetCore;

using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

using Microsoft.OpenApi.Models;

using NineteenSevenFour.Gatehub.Domain.Interfaces;
using NineteenSevenFour.Gatehub.EFCore.SQLite.Services;
using Swashbuckle.AspNetCore.Filters;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization;

using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var CorsPolicyName = "Gatehub security policy";
var SwaggerDocName = "Gatehub api documentation";

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IApplicationService, ApplicationService>();

//TODO: Review those services
//builder.Services.AddHealthChecks();

//builder.Services.AddAntiforgery((options) =>
//{
//});

//builder.Services.AddHsts((options) => {
//  options.IncludeSubDomains = true;
//  options.Preload = true;
//  options.MaxAge = new TimeSpan(1,0,0);
//});

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
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc(
    "v1",
    new OpenApiInfo { Title = SwaggerDocName, Version = "v1" }
    );

  // use it if you want to hide Paths and Definitions from OpenApi documentation correctly
  // see https://github.com/unchase/Unchase.Swashbuckle.AspNetCore.Extensions
  c.UseAllOfToExtendReferenceSchemas();

  // see https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters#installation
  c.ExampleFilters();
  c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

  // Add documentation from XmlDoc using System.Reflection;
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  c.IncludeXmlCommentsWithRemarks(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add FV, see https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// Add FV validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// Add FV Rules to swagger
builder.Services.AddFluentValidationRulesToSwagger();

// see https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters#installation
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
app.MapSwagger();
app.UseSwagger();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();

  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("./v1/swagger.json", SwaggerDocName);
    c.RoutePrefix = "swagger";
  });
}
else
{
  app.UseReDoc(c =>
  {
    c.DocumentTitle = SwaggerDocName;
    c.SpecUrl = "/swagger/v1/swagger.json";
    c.RoutePrefix = "redoc";
  });
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicyName);

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// Gatehub api entry point
/// </summary>
/// <remarks>
///Trick the coverage reporting tool to exclude this file
/// </remarks>
[ExcludeFromCodeCoverage]
public partial class Program { }
