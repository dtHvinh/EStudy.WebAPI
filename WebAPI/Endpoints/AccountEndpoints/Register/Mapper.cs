using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.AccountEndpoints.Register;

[Mapper(AllowNullPropertyAssignment = false)]
public static partial class Mapper
{
    public static partial User ToUser(this RegisterRequest request);
}
