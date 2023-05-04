using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var CorsPolicyName = "Gatehub security policy";
var SwaggerDocName = "Gatehub Api documentation";

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
  // using System.Reflection;
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapSwagger();
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("./v1/swagger.json", SwaggerDocName);
    c.RoutePrefix = "swagger";
  });

  app.UseDeveloperExceptionPage();
}
else
{
  app.UseReDoc(c =>
  {
    c.DocumentTitle = SwaggerDocName;
    c.SpecUrl = "/redoc/index.html";
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
