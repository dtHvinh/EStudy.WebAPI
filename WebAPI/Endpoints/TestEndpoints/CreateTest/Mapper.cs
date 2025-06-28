using Riok.Mapperly.Abstractions;
using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.CreateTest;

[Mapper]
public static partial class Mapper
{
    public static partial TestExam ToTest(this CreateTestRequest request, string creatorId);
}

