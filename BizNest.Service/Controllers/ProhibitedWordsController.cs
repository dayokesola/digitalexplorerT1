using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BizNest.Core.Logic.Definations;
using BizNest.Core.Domain.Model.App;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BizNest.Service.Controllers
{

    [Route("api/[controller]")]
    public class ProhibitedWordsController : Controller
    {
        IProhibitedService service;
        public ProhibitedWordsController(IProhibitedService service)
        {
            this.service = service;
        }
        // Get and query for prohibitted names
        [HttpGet]
        public async Task<IActionResult> Get(string query = "",int skip = 0,int max = 30)
        {
            try
            {
                return Ok(await service.SearchForWords(query, skip, max));
            }
            catch 
            {
                return BadRequest();
            }
           
        }

        

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProhibitedWordModel value)
        {
            try
            {
                await service.InsertWord(value);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ProhibitedWordModel value)
        {
            try
            {
                await service.UpdateWord(value);
                return Ok();
            } 
            catch
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]ProhibitedWordModel value)
        {
            try
            {
                await service.DeleteWord(value);
                return Ok();
            } 
            catch
            {
                return NotFound();
            }
        }
    }
}
