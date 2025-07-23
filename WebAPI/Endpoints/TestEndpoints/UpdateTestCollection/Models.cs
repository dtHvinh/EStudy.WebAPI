namespace WebAPI.Endpoints.TestEndpoints.UpdateTestCollection;

public sealed class UpdateTestCollectionRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
