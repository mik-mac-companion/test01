using GrainInterfaces;
using Orleans.TestingHost;

namespace XCodeTest
{
    [Collection(ClusterCollection.Name)]
    public class UnitTest(ClusterFixture fixture)
    {
        private TestCluster Cluster { get; } = fixture.Cluster;

        [Fact]
        public async Task Test1()
        {
            var hello = Cluster.GrainFactory.GetGrain<IHello>(0);
            var greeting = await hello.SayHello("Hi friend!");

           // Assert.Equal("Hello, World!", greeting);
            Assert.Equal(greeting, "\r\nClient said: \"Hi friend!\", so HelloGrain says: Hello!");
        }
    }
}