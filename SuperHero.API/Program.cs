using FastEndpoints;
using FastEndpoints.Swagger;
using SuperHero.API.Configuration;
using SuperHero.API.ServiceRegistration;
using Serilog;
using SuperHero.API.MiddleWare;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services
    .Configure<AppSettings>(builder.Configuration)
    .AddFastEndpoints()
    .RegisterServices()
    .SwaggerDocument()
    .AddAuthorization()
    .RegisterAuth(builder.Configuration)
    .AddLogging(builder => builder.AddSerilog(Log.Logger))
    ;

var app = builder.Build();
app.UseFastEndpoints()
    .UseSwaggerGen()
    .UseAuthentication()
    .UseAuthorization()
    .UseExceptionHandlingMiddleware();
;
app.Run();