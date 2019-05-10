using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BizNest.Core.Logic.Definations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BizNest.Service.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService service;

        public SearchController(ISearchService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            if (string.IsNullOrEmpty(query) || query.Length < 3) return BadRequest("Please specify a valid business name of not less than 3 chracters to query against");
            return Ok(await service.SearchAsync(query));
        }


    }
}
