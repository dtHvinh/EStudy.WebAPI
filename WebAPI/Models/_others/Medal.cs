using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._others;

[Table("Medals")]
public class Medal
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Icon { get; set; }
    public int Target { get; set; }                 // Target score or number of activities to get the medal
}
