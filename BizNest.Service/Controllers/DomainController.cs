using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizNest.Service.Interfaces;
using BizNest.Service.Models.RequestModels;
using BizNest.Service.Models.ResponseModels;
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
        public async Task<IActionResult> DomainSearch(string domain)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(domain))
                    return BadRequest();

                var reqObj = new DomainSearchModel { Domain = domain.Replace(" ", "").Trim() + ".com" };

                _httpService.SetClientHeader("X-RapidAPI-Host", "domainstatus.p.rapidapi.com");
                _httpService.SetClientHeader("X-RapidAPI-Key", "5acbd0464bmsh17680cb27715eabp13091bjsnf01f91412d73");

                var response = await _httpService.PostAsync<DomainSearchResponse, DomainSearchModel>(reqObj, "https://domainstatus.p.rapidapi.com/");

                return new OkObjectResult(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}