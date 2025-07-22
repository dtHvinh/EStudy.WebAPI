namespace WebAPI.Endpoints.TestEndpoints.AddOrRemoveTestInCollection;

public sealed class AddTestToCollectionRequest
{
    public int CollectionId { get; set; }
    public int TestId { get; set; }
    public bool IsAdd { get; set; } = true;
}