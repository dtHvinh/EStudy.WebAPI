namespace WebAPI.Endpoints.WordEndpoints.CreateWord;

public sealed class CreateWordRequest
{
    public string Text { get; set; } = string.Empty;
}


public sealed class CreateWordResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
}