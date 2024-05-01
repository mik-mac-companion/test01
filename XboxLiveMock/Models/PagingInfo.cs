namespace XboxLiveMock.Models
{
    public record PagingInfo
    {
        public string? ContinuationToken { get; set; }
        public int TotalItems { get; set; }
    }
}
