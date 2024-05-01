using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        //[TestInitialize]
        private IHost SetupDefaultHost()
        {
            string[] args = new string[0];
            IHostBuilder builder = Host.CreateDefaultBuilder(args)
                .UseOrleansClient(client =>
                {
                    client.UseLocalhostClustering();
                })
                .UseConsoleLifetime();

            IHost host = builder.Build();
            host.Start();
            return host;
        }

        [TestMethod]
        public async Task HelloTest()
        {
            using IHost host = SetupDefaultHost();

            IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

            IHello friend = client.GetGrain<IHello>(0);
            string response = await friend.SayHello("Hi friend!");
            Assert.IsTrue(response == "\r\nClient said: \"Hi friend!\", so HelloGrain says: Hello!");
        }

        [TestMethod]
        public async Task MultiplyTest()
        {
            using IHost host = SetupDefaultHost();

            IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

            IHello friend = client.GetGrain<IHello>(0);
            int response = await friend.Multiply(20, 4);
            Assert.IsTrue(response == 80);
            response = await friend.Multiply(320, 4);
            Assert.IsTrue(response == 1280);
        }

        [TestMethod]
        public async Task ModulusTest()
        {
            using IHost host = SetupDefaultHost();

            IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

            IHello friend = client.GetGrain<IHello>(0);
            int response = await friend.AModB(20, 4);
            Assert.IsTrue(response == 0);
            response = await friend.AModB(21, 4);
            Assert.IsTrue(response == 1);
            response = await friend.AModB(22, 4);
            Assert.IsTrue(response == 2);
            response = await friend.AModB(23, 4);
            Assert.IsTrue(response == 3);
            response = await friend.AModB(24, 4);
            Assert.IsTrue(response == 0);
        }

        [TestMethod]
        public async Task GetTest()
        {
            using IHost host = SetupDefaultHost();

            IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

            IHello friend = client.GetGrain<IHello>(0);
            var response = await friend.Pulldata();
            var text = response;//.Result;
            Assert.IsTrue(text == "200");

        }
    }
}