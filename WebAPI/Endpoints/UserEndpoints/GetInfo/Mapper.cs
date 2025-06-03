using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.UserEndpoints.GetInfo;

[Mapper]
public static partial class Mapper
{
    public static partial UserResponse ToResponse(this User user);
}
