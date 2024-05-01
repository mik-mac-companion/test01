namespace Common
{
    public record Response
    {
        public PagingInfo PagingInfo { get; set; }
        public Items Items { get; set; }
    }
}
