using Riok.Mapperly.Abstractions;
using WebAPI.Models;

namespace WebAPI.Endpoints.UserEndpoints.Update;

[Mapper(AllowNullPropertyAssignment = false)]
public static partial class Mapper
{
    public static partial void ApplyUpdate(this UpdateUserRequest request, User user);
}
