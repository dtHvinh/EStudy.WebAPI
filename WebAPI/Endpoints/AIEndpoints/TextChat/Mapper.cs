using OllamaSharp.Models.Chat;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Endpoints.AIEndpoints.TextChat;

[Mapper]
public static partial class Mapper
{
    public static partial List<Message> ToMessageList(this List<MessageRequest> message);
}
