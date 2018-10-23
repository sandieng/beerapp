using System.IdentityModel.Tokens.Jwt;

namespace BeerRosterAPI.Services
{
    public interface IJwtService
    {
        string GenerateJwt(string userName);
        string UpdateJwt(string bearerToken);
        JwtSecurityToken DecodeJwt(string token);
        string GetUpdatedJwt();
    }
}