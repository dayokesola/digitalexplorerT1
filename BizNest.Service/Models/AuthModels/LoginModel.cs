using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizNest.Service.Models.AuthModels
{
    public class LoginModel
    {
        [Required]
#if DEBUG
        [DefaultValue("dev@biznest.local")]
#endif
        [JsonProperty("email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
#if DEBUG
        [DefaultValue("Dev@12345")]
#endif
        public string Password { get; set; }

    }
}
