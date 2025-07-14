using FastEndpoints;

namespace WebAPI.Endpoints.StorageEndpoints;

public class StorageGroup : Group
{
    public StorageGroup()
    {
        Configure("storage", cf =>
        {
        });
    }
}
