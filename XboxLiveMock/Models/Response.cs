using XboxLiveMock.Models;

public record Response
{
    public PagingInfo PagingInfo { get; set; } = new();
    public Items Items { get; set; } = new();
}