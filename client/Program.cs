using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;
using Microsoft.Extensions.Configuration;
using Common;

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
    .UseOrleansClient(client =>
    {
        client.UseLocalhostClustering()
            .ConfigureServices((services) =>
        {
            // Retrieve the registered AppSettings
            var appSettings = services.BuildServiceProvider().GetRequiredService<AppSettings>();

            // Register AppSettings in the Orleans DI container
            services.AddSingleton(appSettings);
        }); ;
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();


using IHost host = builder.Build();

await host.StartAsync();

IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

IHello friend = client.GetGrain<IHello>(0);
/*string response = await friend.SayHello("Hi friend!");

Console.WriteLine($"""
    {response}

    Press any key to exit...
    """);

Console.ReadKey();

int response2 = await friend.Multiply(20, 4);

Console.WriteLine($"""
    {response2}
    Press any key to exit...
    """);

Console.ReadKey();

int response3 = await friend.AModB(21, 4);

Console.WriteLine($"""
    {response3}
    Press any key to exit...
    """);

Console.ReadKey();*/

var response4 = await friend.Pulldata();
var response5 = response4.ToString();

Console.WriteLine($"""
    {response5}
    Press any key to exit...
    """);

Console.ReadKey();

await host.StopAsync();