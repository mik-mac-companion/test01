using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Bind AppSettings from appsettings.json
        var appSettings = hostContext.Configuration.Get<AppSettings>();
        services.AddSingleton(appSettings);
    })
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole())
            .ConfigureServices((services) =>
            {
                // Retrieve the registered AppSettings
                var appSettings = services.BuildServiceProvider().GetRequiredService<AppSettings>();

                // Register AppSettings in the Orleans DI container
                services.AddSingleton(appSettings);
            });
    })
    .UseConsoleLifetime();

using IHost host = builder.Build();

await host.RunAsync();