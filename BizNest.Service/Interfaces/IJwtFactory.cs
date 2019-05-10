using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizNest.Service.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string username, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string username, string id);

        ClaimsIdentity GenerateSysClaimsIdentity(string appname, string appkey);
    }
}
