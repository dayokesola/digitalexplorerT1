using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizNest.Core.Logic.Definations;
using Microsoft.AspNetCore.Mvc;

using BizNest.Core.Domain.Model.App;
using BizNest.Core.Domain.Entity.App;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BizNest.Service.Controllers
{
    [Route("api/[controller]")]
    public class BusinessController : Controller
    {
        private readonly IBusinessService service;
        private readonly IStakeHolderService stakeHolderService;
        private readonly IEmailService _emailService;

        public BusinessController(
            IBusinessService service,
            IStakeHolderService stakeHolderService,
            IEmailService emailService)
        {
            this.service = service;
            this.stakeHolderService = stakeHolderService;
            _emailService = emailService;        }


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

        [HttpPost("createbusiness")]
        public async Task<IActionResult> PostAsync([FromBody] BusinessCreateModel model)
        {
            try
            {
                var bm = new BusinessModel
                {
                    Name = model.BusinessName,
                    Code = model.AddressPostCode,
                    AddressStreet = model.AddressStreet,
                    AddressCity = model.AddressCity,
                    AddressPostCode = model.AddressPostCode,
                    Contact1Name = model.Contact1Name,
                    Contact1Email = model.Contact1Email,
                    Contact1Mobile = model.Contact1Mobile,
                    Contact2Email = model.Contact2Email,
                    Contact2Name = model.Contact2Name,
                    Contact2Mobile = model.Contact2Name,
                    Status = RegistrationStatus.Submitted,
                    BusinessTypeId = 1
                    
                };

                await service.InsertBusinessAsync(bm);

                foreach (var item in model.StakeHolders)
                {
                    var stk = new StakeHolderModel
                    {
                        BusinessId = bm.Id,
                        SSN = item.SSN,
                        FirstName = item.FirstName,
                        LastName = item.FirstName,
                        Email = item.Email,
                        Mobile = item.Mobile,
                        BirthDate = item.BirthDate,
                        AddressStreet = item.AddressStreet,
                        AddressCity = item.AddressCity,
                        AddressPostCode = item.AddressPostCode,
                        SeedCapital = item.SeedCapital,
                        BankName = item.BankName,
                        AccountNumber = item.AccountNumber
                    };

                    await stakeHolderService.InsertAsync(stk);

                    
                }
                await _emailService.SendEmailAsync(model.Contact1Email, null, "Business Search", "Hello " + model.Contact1Name + ", " +
                    "your application has been recieved by Yellow Project " +
                    "and our person will get accross to you. Thank you!");
                return new OkResult();

            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }


    }
}
