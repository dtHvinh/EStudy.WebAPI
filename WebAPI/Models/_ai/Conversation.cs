using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._ai;

[Table("Conversations")]
public class Conversation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastActive { get; set; } = DateTimeOffset.UtcNow;

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public ICollection<ConversationMessage> Messages { get; set; } = default!;
}
