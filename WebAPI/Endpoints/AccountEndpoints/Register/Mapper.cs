using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.AccountEndpoints.Register;

[Mapper(AllowNullPropertyAssignment = false)]
public static partial class Mapper
{
    [MapValue(nameof(User.CreationDate), Use = nameof(GetNow))]
    public static partial User ToUser(this RegisterRequest request);

    static DateTimeOffset GetNow() => DateTimeOffset.UtcNow;
}
