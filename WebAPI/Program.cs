using FastEndpoints;
using Serilog;
using System.Reflection;
using System.Text.Json;
using WebAPI.Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/webapi-.log", rollingInterval: RollingInterval.Day));

SeptupExtensions.Config = builder.Configuration;

builder.Services.AddOpenApi();
builder.Services.AddClients();
builder.Services.AddFastEndpoints(cf =>
{
    cf.Filter = e => e.GetCustomAttribute<ObsoleteAttribute>() is null;
});

builder.Services.ConfigureCors();
builder.Services.ConfigureDatabase();
builder.Services.ConfigureJwtAuthentication();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("Default");

app.UseDefaultExceptionHandler().UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

app.Run();
