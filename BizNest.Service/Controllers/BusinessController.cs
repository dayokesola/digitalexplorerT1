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
    public class BusinessController : Controller
    {
        private readonly IBusinessService service;
        private readonly IStakeHolderService stakeHolderService;

        public BusinessController(IBusinessService service,IStakeHolderService stakeHolderService)
        {
            this.service = service;
            this.stakeHolderService = stakeHolderService;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string query, string contact, string code, int skip, int limit = 20) => Ok(await service.SearchForBusinessAsync(query, contact, code, skip, limit));




        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id, bool includeAll = false)
        {
            var item = await service.GetById(id);
            if (item is null) return NotFound();
            if (includeAll) item.StakeHolders = await stakeHolderService.GetStakeHoldersByBusinessId(item.Id);
            return Ok(item);
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var item = await service.GetByToken(code);
            if (item is null) return NotFound();
            else return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BusinessModel value)
        {
            try
            {
                return Ok(await service.InsertBusinessAsync(value));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]BusinessModel value)
        {
            try
            {
                return Ok(await service.UpdateBusinessAsync(value));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> PutStatus([FromBody] BusinessStatusChangeModel value)
        {
            try
            {
                await service.ChangeStatus(value);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> PostAsync([FromBody] BusinessCreateModel model)
        {
            try
            {
                
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }


    }
}
