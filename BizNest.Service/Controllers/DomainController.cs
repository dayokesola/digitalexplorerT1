using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizNest.Service.Interfaces;
using BizNest.Service.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BizNest.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private IHttpService _httpService;
        public DomainController(IHttpService httpService)
        {
            _httpService = httpService;
        }
        [HttpPost("domainsearch")]
        public IActionResult DomainSearch(string domain)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(domain))
                    return null;
                var reqObj = new DomainSearchModel { Domain = domain.Replace(" ", "").Trim() + ".com" };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}