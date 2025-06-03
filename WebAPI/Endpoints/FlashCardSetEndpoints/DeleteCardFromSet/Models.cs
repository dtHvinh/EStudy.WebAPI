namespace WebAPI.Endpoints.FlashCardSetEndpoints.DeleteCardFromSet;

public sealed class DeleteCardFromSetRequest
{
    public int CardId { get; set; }
    public int SetId { get; set; }
}
