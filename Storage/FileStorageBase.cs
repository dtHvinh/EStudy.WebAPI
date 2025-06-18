namespace WebAPI.Storage;

public abstract class FileStorageBase(string url, string key)
{
    private readonly string _url = url;
    private readonly string _key = key;
    private Supabase.Client? _client;

    protected Supabase.Client Client
    {
        get
        {
            if (_client == null)
            {
                throw new InvalidOperationException("Client is not initialized");
            }
            return _client;
        }
    }

    public async void InitializeAsync()
    {
        _client = new Supabase.Client(_url, _key);
        await _client.InitializeAsync();
    }
}

