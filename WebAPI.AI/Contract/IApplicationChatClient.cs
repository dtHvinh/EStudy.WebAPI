namespace WebAPI.AI.Contract;

public interface IApplicationChatClient
{
    TClient GetClient<TClient>();
}
