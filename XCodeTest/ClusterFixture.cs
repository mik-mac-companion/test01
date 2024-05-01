using Orleans.TestingHost;

namespace XCodeTest
{
    public sealed class ClusterFixture : IDisposable
    {
        public TestCluster Cluster { get; } = new TestClusterBuilder().Build();

        public ClusterFixture()
        {
            Cluster.Deploy();
        }
        public void Dispose() => Cluster.StopAllSilos();
    }
}