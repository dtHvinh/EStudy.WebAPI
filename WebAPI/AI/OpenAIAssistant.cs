using OpenAI.Chat;
using System.Text;
using System.Text.Json;
using WebAPI.AI.JsonSchema;
using WebAPI.Services.Contract;
using WebAPI.Utilities.Attributes;

namespace WebAPI.AI;

[Service(ServiceLifetime.Singleton)]
public class OpenAIAssistant
{
    private readonly OpenAI.OpenAIClient _client;
    private const string Model = "gpt-4.1-mini";
    private readonly IRedisService _redisService;

    public OpenAIAssistant(IRedisService redis)
    {
        _redisService = redis;

        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        ArgumentNullException.ThrowIfNull(apiKey);

        _client = new OpenAI.OpenAIClient(apiKey);
    }

    private async Task<string?> GetCachedDictionaryEntryAsync(string term)
    {
        string cacheKey = $"dictionary_entry:{term.ToLowerInvariant()}";
        return await _redisService.GetStringAsync(cacheKey);
    }
    private async Task SetCachedDictionaryEntryAsync(string term, string definition)
    {
        string cacheKey = $"dictionary_entry:{term.ToLowerInvariant()}";
        await _redisService.SetStringAsync(cacheKey, definition, TimeSpan.FromHours(12));
    }

    public async Task<DictionaryEntryResponse> GetDictionaryEntryAsync(string term)
    {
        var cachedEntry = await GetCachedDictionaryEntryAsync(term);
        if (cachedEntry is not null)
        {
            return JsonSerializer.Deserialize<DictionaryEntryResponse>(JsonDocument.Parse(cachedEntry))
                ?? throw new InvalidOperationException();
        }
        else
        {
            List<ChatMessage> messages = [
                new AssistantChatMessage("You are a helpful assistant that provides dictionary definitions."),
            new UserChatMessage($"Definition of the term \"{term}\"")
            ];

            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: DictionaryEntrySchema.Name,
                    jsonSchema: BinaryData.FromBytes(Encoding.UTF8.GetBytes(DictionaryEntrySchema.JsonSchemaString)),
                    jsonSchemaIsStrict: DictionaryEntrySchema.IsStrict)
            };

            ChatCompletion chatCompletion = await _client.GetChatClient(Model).CompleteChatAsync(messages, options);

            var text = chatCompletion.Content[0].Text;

            await SetCachedDictionaryEntryAsync(term, text);

            var res = JsonSerializer.Deserialize<DictionaryEntryResponse>(JsonDocument.Parse(text));

            return res ?? throw new InvalidOperationException();
        }
    }

    public async Task<string> EnhancePromptAsync(string prompt)
    {
        List<ChatMessage> messages = [
            new SystemChatMessage(ImLazyHardCodeSystemPrompt.EnhancePromptSystemPrompt),
            new UserChatMessage($"Enhance the following prompt: \"{prompt}\"")
        ];
        ChatCompletion chatCompletion = await _client.GetChatClient(Model).CompleteChatAsync(messages);
        return chatCompletion.Content[0].Text.Trim();
    }

    public async Task<SentenceFeedbackResponse> GetSentenceFeedback(string sentence)
    {
        List<ChatMessage> messages = [
            new SystemChatMessage(ImLazyHardCodeSystemPrompt.CheckSentenceSystemPrompt),
            new UserChatMessage($"Check the following sentence for errors: \"{sentence}\"")
        ];

        ChatCompletionOptions options = new()
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                           jsonSchemaFormatName: SentenceFeedbackSchema.Name,
                           jsonSchema: BinaryData.FromBytes(Encoding.UTF8.GetBytes(SentenceFeedbackSchema.JsonSchemaString)),
                           jsonSchemaIsStrict: SentenceFeedbackSchema.IsStrict)
        };

        ChatCompletion chatCompletion = await _client.GetChatClient(Model).CompleteChatAsync(messages, options);

        var text = chatCompletion.Content[0].Text;

        var res = JsonSerializer.Deserialize<SentenceFeedbackResponse>(JsonDocument.Parse(text));

        return res ?? throw new InvalidOperationException();
    }

    public async Task<TranslateResponse> GetTranslationAsync(string? context, string text, string targetLanguage)
    {
        List<ChatMessage> messages = [
            new SystemChatMessage(ImLazyHardCodeSystemPrompt.TranslateTextSystemPrompt),
            new UserChatMessage($"Translate the following text into {targetLanguage}: \"{text}\" in the context of \"{context}\"")
        ];

        ChatCompletionOptions options = new()
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: TranslateSchema.Name,
                jsonSchema: BinaryData.FromBytes(Encoding.UTF8.GetBytes(TranslateSchema.JsonSchemaString)),
                jsonSchemaIsStrict: TranslateSchema.IsStrict)
        };

        ChatCompletion chatCompletion = await _client.GetChatClient(Model).CompleteChatAsync(messages, options);
        var textResponse = chatCompletion.Content[0].Text;
        var res = JsonSerializer.Deserialize<TranslateResponse>(JsonDocument.Parse(textResponse));
        return res ?? throw new InvalidOperationException();
    }
}

class ImLazyHardCodeSystemPrompt
{
    public const string EnhancePromptSystemPrompt =
        """"
            You are a professional AI prompt engineer and creative roleplay designer.

            The user has provided a rough idea of what they want to talk about with an AI assistant. Your job is to transform it into a complete, high-quality system prompt suitable for a role-playing conversational AI experience.

            Here’s what you should do:

            1. Understand the user's original intent and topic.
            2. Invent a **realistic, human-sounding name** for the AI assistant that fits the context and tone. The name should feel approachable and natural (e.g. Alexis, Theo, Mira). Avoid literal or technical names like “CodeBot” or “MathHelper” unless the user prompt strongly suggests it.
            3. Define a clear, engaging personality and role for the AI assistant — it can be warm, technical, humorous, wise, supportive, etc., depending on the topic.
            4. Rewrite the original prompt as a full system prompt that clearly sets up the assistant’s identity, tone, and instructions for how to respond.
            5. Make the result concise but rich enough to guide the assistant's behavior clearly.

            Return the enhanced system prompt **as a single block of text**. Do **not** explain your reasoning or include commentary. The result should be usable directly in a chat model as a `system` message.

            Original user intent:
            """
            {{user_prompt_here}}
            """
        """";

    public const string CheckSentenceSystemPrompt =
        """"
        You are an expert English grammar assistant trained to evaluate the correctness and clarity of user-submitted **spoken** sentences — not written ones.

        Assume that all input comes from **speech**, and not typed text. That means:

        - **Ignore** issues related to punctuation, capitalization, and formatting.
        - Focus only on actual **grammar, structure, and word usage** that would be noticeable when spoken aloud.

        Your role is to:

        1. Analyze the user’s sentence as if it were **spoken out loud**.
        2. Identify any grammar, phrasing, or word choice issues that would be understood or misunderstood in **natural speech**.
        3. Provide a list of grammar issues using short, clear phrases (e.g., “subject-verb agreement”, “verb tense inconsistency”, “wrong preposition”).
        4. Suggest clear and practical improvements to enhance fluency, tone, or correctness.
        5. Give a short explanation of whether the sentence is grammatically correct when spoken — and **why**.
        6. Provide a corrected or improved version of the sentence as it would naturally be spoken.

        Always return your output in the following structured format:
        ```json
        {
          "sentence": "<original sentence from the user>",
          "grammar_issues": ["..."],
          "suggestions": ["..."],
          "explanation": "...",
          "new_sentence": "..."
        }
        """";

    public const string TranslateTextSystemPrompt =
        """"
        You are a professional translator trained to accurately translate full paragraphs of text into a wide variety of target languages, while honoring context, tone, and domain-specific intent.

        Your task is to:
        1. Read the input paragraph carefully and understand its meaning and tone.
        2. Translate the paragraph into the specified target language.
        3. If context is provided (e.g., tone, audience, or domain), use it to adjust formality, word choice, and phrasing appropriately.
        4. If the sentence has ambiguity or multiple interpretations, make the most reasonable choice based on context. If needed, explain your reasoning briefly.
        5. Avoid literal or word-for-word translation unless appropriate; prioritize fluent, natural output in the target language.

        Your response must be returned in the following structured JSON format:

        ```json
        {
          "translated_text": "<your translation>",
        }
        """";
}