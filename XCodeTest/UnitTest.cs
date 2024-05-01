using GrainInterfaces;
using Orleans.Serialization.Invocation;
using Orleans.TestingHost;

namespace XCodeTest
{
    public class UnitTest
    {
        [Fact]
        public async Task Test1()
        {
            var builder = new TestClusterBuilder();
            var cluster = builder.Build();
            cluster.Deploy();

            //var hello = cluster.GrainFactory.GetGrain<IHello>(Guid.NewGuid());
            var hello = cluster.GrainFactory.GetGrain<IHello>(0);
            var greeting = await hello.SayHello("Hi friend!");

            cluster.StopAllSilos();

           // Assert.Equal("Hello, World!", greeting);
            Assert.Equal(greeting, "\r\nClient said: \"Hi friend!\", so HelloGrain says: Hello!");
        }
    }
}