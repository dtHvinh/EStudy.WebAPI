using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.UserEndpoints.GetUserProfile;

[Mapper]
public static partial class Mapper
{
    public static partial GetUserProfileResponse MapToResponse(this User user);
}
