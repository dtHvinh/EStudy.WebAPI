using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._ai;

[Table("ConversationMessages")]
public class ConversationMessage
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsUserMessage { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public int ConversationId { get; set; }
    [ForeignKey(nameof(ConversationId))]
    public Conversation Conversation { get; set; } = default!;
}
