namespace WebAPI.Endpoints.FlashCardEndpoints.DeleteCardFromSet;

public sealed class DeleteCardFromSetRequest
{
    public int SetId { get; set; }
    public int CardId { get; set; }
}
