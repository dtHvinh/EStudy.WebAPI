#nullable disable

using System.Text.Json.Serialization;

namespace WebAPI.AI.JsonSchema;

public class TranslateSchema
{
    public const string Name = "translate_text";
    public const bool IsStrict = true;
    public const string JsonSchemaString = """
        {
        "type": "object",
        "properties": {
            "translated_text": {
                "type": "string",
                "description": "The translated text."
            }
        },
        "required": ["translated_text"],
        "additionalProperties": false
        }
    """;
}

public class TranslateResponse
{
    [JsonPropertyName("translated_text")]
    public string TranslatedText { get; set; }
}