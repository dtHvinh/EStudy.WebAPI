#nullable disable

using System.Text.Json.Serialization;

namespace WebAPI.AI.JsonSchema;

public class SentenceFeedbackSchema
{
    public const string Name = "sentence_feedback";
    public const bool IsStrict = true;
    public const string JsonSchemaString = """
        {
          "type": "object",
          "properties": {
            "sentence": {
              "type": "string",
              "description": "The original sentence submitted by the user."
            },
            "grammar_issues": {
              "type": "array",
              "items": {
                "type": "string"
              },
              "description": "List of grammar or usage issues found in the sentence."
            },
            "suggestions": {
              "type": "array",
              "items": {
                "type": "string"
              },
              "description": "Suggestions to improve or correct the sentence."
            },
            "explanation": {
              "type": "string",
              "description": "Explanation of whether and why the user's sentence is correct or incorrect."
            },
            "new_sentence": {
              "type": "string",
              "description": "A correctly phrased version of the user's sentence, or an improved alternative."
            }
          },
          "required": ["sentence", "grammar_issues", "suggestions", "explanation", "new_sentence"],
          "additionalProperties": false
        }
        """;
}

public class SentenceFeedbackResponse
{
    [JsonPropertyName("sentence")]
    public string Sentence { get; set; }

    [JsonPropertyName("grammar_issues")]
    public List<string> GrammarIssues { get; set; }

    [JsonPropertyName("suggestions")]
    public List<string> Suggestions { get; set; }

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; }

    [JsonPropertyName("new_sentence")]
    public string NewSentence { get; set; }
}