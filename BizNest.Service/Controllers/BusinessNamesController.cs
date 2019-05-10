using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model.App;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BizNest.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessNamesController : ControllerBase
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Add Business
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Search")]
        [HttpPost] 
        public ActionResult<List<BusinessNameSearchModel>> Search(BusinessNameSearchForm form)
        {
            try
            {
                //do some search here
                var dto = new List<BusinessNameSearchModel>
                {
                    new BusinessNameSearchModel()
                    {
                        Name = "NAme 1",
                        Match = 60.28
                    },
                    new BusinessNameSearchModel()
                    {
                        Name = "Name 2",
                        Match = 45.45
                    },
                }; 
                return Ok(dto);
            }
            catch (Exception ex)
            {
                 
                return BadRequest(ex.Message);
            }
        }
    }
}