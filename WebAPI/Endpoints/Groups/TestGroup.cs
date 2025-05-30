using FastEndpoints;

namespace WebAPI.Endpoints.Groups;

public class TestGroup : Group
{
    public TestGroup()
    {
        Configure("test", cf =>
        {
        });
    }
}
