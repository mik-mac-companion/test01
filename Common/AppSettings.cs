namespace Common
{
    public class AppSettings
    {
        public ServicesSettings? Services { get; set; }
    }

    public class ServicesSettings
    {
        public XboxLiveSettings? XboxLive { get; set; }
        public FairlightSettings? Fairlight { get; set; }
    }

    public class XboxLiveSettings
    {
        public string? Url { get; set; }
    }

    public class FairlightSettings
    {
        public string? Url { get; set; }
    }
}
