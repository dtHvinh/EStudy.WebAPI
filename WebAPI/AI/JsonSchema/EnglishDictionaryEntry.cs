namespace WebAPI.AI.JsonSchema;

public sealed class EnglishDictionaryEntry
{
    public string Word { get; set; } = string.Empty;
    public string? Phonetic { get; set; }
    public List<Phonetic>? Phonetics { get; set; }
    public List<Meaning> Meanings { get; set; } = default!;
    public List<string>? SourceUrls { get; set; }
}

public sealed class Phonetic
{
    public string? Text { get; set; }
    //public string? Audio { get; set; } // URL to pronunciation audio
}

public sealed class Meaning
{
    public string PartOfSpeech { get; set; } = string.Empty; // e.g. noun, verb
    public List<Sense> Definitions { get; set; }
}

public sealed class Sense
{
    public string Definition { get; set; } = string.Empty;
    public List<string>? Examples { get; set; }
    public List<string>? Synonyms { get; set; }
    public List<string>? Antonyms { get; set; }
    public List<Translation>? Translations { get; set; }
}

public sealed class Translation
{
    public string Language { get; set; } = string.Empty; // e.g. "es"
    public string Text { get; set; } = string.Empty;      // translated word
}
