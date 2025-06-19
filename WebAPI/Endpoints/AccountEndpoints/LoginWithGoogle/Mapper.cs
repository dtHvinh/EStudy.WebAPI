using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.AccountEndpoints.LoginWithGoogle;

[Mapper]
public static partial class Mapper
{
    [MapValue(nameof(User.CreationDate), Use = nameof(GetNow))]
    [MapValue(nameof(User.UserName), Use = nameof(GetRandomUsername))]
    [MapProperty(nameof(GoogleUserInfoResponse.Picture), nameof(User.ProfilePicture))]
    public static partial User ToUser(this GoogleUserInfoResponse source);

    static DateTimeOffset GetNow() => DateTimeOffset.UtcNow;
    static string GetRandomUsername() => Fn.RandomString(5);
}
