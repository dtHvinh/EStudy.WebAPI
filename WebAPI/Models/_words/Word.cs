using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._words;

[Table("Words")]
[Index(nameof(Text))]
public class Word
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
}
