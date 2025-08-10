#nullable disable

namespace WebAPI.Endpoints.AIEndpoints.GetTerm;

public sealed class GetTermRequest
{
    public required string Term { get; init; } = null!;
}

public enum PartOfSpeech
{
    noun,
    verb,
    adjective,
    adverb,
    pronoun,
    preposition,
    conjunction,
    interjection,
    determiner
}