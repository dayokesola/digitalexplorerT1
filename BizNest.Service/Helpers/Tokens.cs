using BizNest.Service.Interfaces;
using BizNest.Service.Models.AuthModels;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizNest.Service.Helpers
{
    public static class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            return await jwtFactory.GenerateEncodedToken(userName, identity);
        }
    }
}
