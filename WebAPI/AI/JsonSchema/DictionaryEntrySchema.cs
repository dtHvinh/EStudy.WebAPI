#nullable disable

using System.Text.Json.Serialization;

namespace WebAPI.AI.JsonSchema;

public class DictionaryEntrySchema
{
    public const string Name = "english_dictionary_entry";
    public const bool IsStrict = true;
    public const string JsonSchemaString = """
        {
            "type": "object",
            "properties": {
              "word": {
                "type": "string",
                "description": "The word being defined.",
                "minLength": 1
              },
              "part_of_speech": {
                "type": "string",
                "description": "The grammatical part of speech of the word.",
                "enum": [
                  "noun",
                  "verb",
                  "adjective",
                  "adverb",
                  "pronoun",
                  "preposition",
                  "conjunction",
                  "interjection",
                  "determiner"
                ]
              },
              "pronunciation": {
                "type": "string",
                "description": "Phonetic pronunciation using IPA."
              },
              "definitions": {
                "type": "array",
                "description": "List of definitions for the word.",
                "items": {
                  "type": "object",
                  "properties": {
                    "definition": {
                      "type": "string",
                      "description": "The meaning of the word."
                    },
                    "example": {
                      "type": "string",
                      "description": "Example sentence using the word."
                    }
                  },
                  "required": [
                    "definition",
                    "example"
                  ],
                  "additionalProperties": false
                }
              },
              "synonyms": {
                "type": "array",
                "description": "List of synonyms for the word.",
                "items": {
                  "type": "string",
                  "minLength": 1
                }
              },
              "antonyms": {
                "type": "array",
                "description": "List of antonyms for the word.",
                "items": {
                  "type": "string",
                  "minLength": 1
                }
              }
            },
            "required": [
              "word",
              "part_of_speech",
              "pronunciation",
              "definitions",
              "synonyms",
              "antonyms"
            ],
            "additionalProperties": false
        }
        """;
}


public class DictionaryEntryResponse
{
    [JsonPropertyName("word")]
    public string Word { get; set; }

    [JsonPropertyName("part_of_speech")]
    public string PartOfSpeech { get; set; }

    [JsonPropertyName("pronunciation")]
    public string Pronunciation { get; set; }

    [JsonPropertyName("definitions")]
    public List<DefinitionItem> Definitions { get; set; }

    [JsonPropertyName("synonyms")]
    public List<string> Synonyms { get; set; }

    [JsonPropertyName("antonyms")]
    public List<string> Antonyms { get; set; }
}

public class DefinitionItem
{
    [JsonPropertyName("definition")]
    public string Definition { get; set; }

    [JsonPropertyName("example")]
    public string Example { get; set; }
}
