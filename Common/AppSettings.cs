namespace Common
{
    public class AppSettings : IAppSettings
    {
        public ServiceSettings? Services { get; set; }
    }

    public class ServiceSettings
    {
        public XboxLiveSettings? XboxLive { get; set; }
    }

    public class XboxLiveSettings
    {
        public string? Url { get; set; }
    }

    public interface IAppSettings
    {
        ServiceSettings? Services { get; set; }
    }
}
