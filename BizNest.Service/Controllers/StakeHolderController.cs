using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizNest.Core.Logic.Definations;
using Microsoft.AspNetCore.Mvc;

using BizNest.Core.Domain.Model.App;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BizNest.Service.Controllers
{
    [Route("api/[controller]")]
    public class StakeHolderController : Controller
    {
        private readonly IStakeHolderService service;

        public StakeHolderController(IStakeHolderService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await service.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
           
            var item = await service.GetSingleAsync(id);
            if (item is null) return NotFound();
            return Ok(item);
            
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StakeHolderModel value)
        {
            try
            {
                return Ok(await service.InsertAsync(value));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]StakeHolderModel value)
        {
            try
            {
                await service.UpdateAsync(value);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await service.DeleteAsync(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}
