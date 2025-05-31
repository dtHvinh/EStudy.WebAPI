using WebAPI.Models.Contract;

namespace WebAPI.Models;

public class Resource : IEntityWithTime<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ResourceUrl { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}
