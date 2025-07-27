using System.Text.Json.Serialization;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.WordEndpoints.GetWord;

public sealed class GetWordRequest : PaginationParams
{
    public string? Name { get; set; }
}

public sealed class GetWordResponse
{
    public int Id { get; set; }
    public required string Text { get; set; }

    [JsonIgnore]
    public float Rank { get; set; }
}

