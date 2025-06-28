using FastEndpoints;

namespace WebAPI.Endpoints.TestEndpoints;

public class TestGroup : Group
{
    public TestGroup()
    {
        Configure("tests", cf =>
        {
        });
    }
}
