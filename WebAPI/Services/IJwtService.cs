using System.Security.Claims;
using WebAPI.Models._others;

namespace WebAPI.Services;

public interface IJwtService
{
    string GenerateRefreshToken();

    /// <summary>
    /// Generates a JWT token for the specified user
    /// </summary>
    /// <param name="user">The user to generate token for</param>
    /// <returns>JWT token string</returns>
    string GenerateToken(User user);

    /// <summary>
    /// Generates a JWT token based on claims
    /// </summary>
    /// <param name="claims">The claims to include in the token</param>
    /// <returns>JWT token string</returns>
    string GenerateToken(IEnumerable<Claim> claims);
}