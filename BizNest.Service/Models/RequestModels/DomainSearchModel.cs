using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizNest.Service.Models.RequestModels
{
    public class DomainSearchModel
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
