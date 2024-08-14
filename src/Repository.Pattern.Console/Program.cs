
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;

using Castle.Core.Logging;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Repository.Pattern.Entities;
using Repository.Pattern.Entities.Contexts;
using Repository.Pattern.Repositories;
using Repository.Pattern.Repositories.Interfaces;

using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSerilog((serviceProvider, loggerConfiguration) => {
    var assembly = Assembly.GetExecutingAssembly();
    var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
    var version = fvi.FileVersion;

    loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithProperty("Version", version);
});

builder.Services.AddDbContext<RepositoryContext>((service, options) =>
{
    var configuration = service.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("Sql");

    options.UseNpgsql(connectionString)
        .UseLazyLoadingProxies(false)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IRepository<Car>, EntityFrameworkRepository<Car>>();

var host = builder.Build();

var hostApplicationLifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

hostApplicationLifetime.ApplicationStarted.Register(async () => {

    var serviceProvider = builder.Services.BuildServiceProvider();

    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    
    var repository = serviceProvider.GetRequiredService<IRepository<Car>>();

    var car = await repository.GetAsync(x => x.Name == "Xpto",
        cancellationToken: hostApplicationLifetime.ApplicationStopped);

    logger.LogInformation("{car}", JsonSerializer.Serialize(car));

    await host.StopAsync(hostApplicationLifetime.ApplicationStopped);
});

await host.RunAsync(hostApplicationLifetime.ApplicationStopped);
