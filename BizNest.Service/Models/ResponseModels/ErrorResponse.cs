using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizNest.Service.Models.ResponseModels
{
    public class ErrorResponse
    {
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; } = "An Error Occured";
    }
}
