namespace Common
{
    public record Items
    {
        public string? Url { get; set; }
        public string? ItemType { get; set; }
        public string? TitleId { get; set; }
        public string? Containers { get; set; }
        public DateTimeOffset? Obtained { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? State { get; set; }
    }
}
