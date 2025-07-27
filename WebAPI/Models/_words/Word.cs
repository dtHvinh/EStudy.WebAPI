using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._words;

[Table("Words")]
public class Word
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public NpgsqlTsVector SearchVector { get; set; } = default!;
}
