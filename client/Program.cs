using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(client =>
    {
        client.UseLocalhostClustering();
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

var test = await friend.PullHttpsData();

var response4 = await friend.Pulldata();
var response5 = response4.ToString();

Console.WriteLine($"""
    {response5}
    Press any key to exit...
    """);

Console.ReadKey();

await host.StopAsync();