using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizNest.Service.Models.ResponseModels
{
    public class DomainSearchResponse
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tld")]
        public string TLD { get; set; }
        [JsonProperty("tld_valid")]
        public string TLD_Valid { get; set; }
        [JsonProperty("available")]
        public string Available { get; set; }
    }
}
