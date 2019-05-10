using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizNest.Service.Models.ResponseModels
{
    public class LoginResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
