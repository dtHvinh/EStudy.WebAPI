using OllamaSharp.Models.Chat;

namespace WebAPI.AI.Extensions;

public static class EnglishAssistantExtensions
{
    public static List<Message> WithSysPrompt(this List<Message> messages, string prompt)
    {
        var systemPrompt = new Message
        {
            Role = ChatRole.System,
            Content = prompt
        };
        messages.Insert(0, systemPrompt);
        return messages;
    }
}
