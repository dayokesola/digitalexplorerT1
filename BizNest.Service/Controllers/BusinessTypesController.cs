using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BizNest.Core.Common;
using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model.App;
using BizNest.Core.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BizNest.Service.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BusinessTypesController : BaseApiController
    {
        public BusinessTypesController(AppDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Search, Page, filter and Shaped BusinessTypes
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="name"></param>
        /// <param name="minStakeHolder"></param>
        /// <param name="maxStakeHolder"></param>
        /// <param name="minCapital"></param>
        /// <param name="info"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="fields"></param>
        /// <param name="draw"></param>
        /// <returns></returns>  

        [HttpGet("Search")] 
        public IActionResult Get(string sort = "id", string name = "", int minStakeHolder = 0, int maxStakeHolder = 0, decimal minCapital = 0, string info = "", long page = 1, long pageSize = 10, string fields = "", int draw = 1)
        {
            try
            {
                var items = Logic.BusinessTypeService.SearchView(name, minStakeHolder, maxStakeHolder, minCapital, info, page, pageSize, sort);

                if (page > items.TotalPages) page = items.TotalPages;
                var jo = new JObjectHelper();
                jo.Add("name", name);
                jo.Add("minStakeHolder", minStakeHolder);
                jo.Add("maxStakeHolder", maxStakeHolder);
                jo.Add("minCapital", minCapital);
                jo.Add("info", info);

                jo.Add("fields", fields);
                jo.Add("sort", sort);
                //var urlHelper = new UrlHelper(Request);
                //var linkBuilder = new PageLinkBuilder(urlHelper, "BusinessTypeApi", jo, page, pageSize, items.TotalItems, draw);
                //AddHeader("X-Pagination", linkBuilder.PaginationHeader);
                var dto = new List<BusinessTypeModel>();
                if (items.TotalItems <= 0) return Ok(dto);
                var dtos = items.Items.ShapeList(fields);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get BusinessType by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Detail/{id}")] 
        public ActionResult Get(int id)
        {
            try
            {
                var item = Logic.BusinessTypeService.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add BusinessType
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>  
        [HttpPost("Create")]
        public IActionResult Create(BusinessTypeForm form)
        {
            try
            {
                var model = Logic.BusinessTypeService.Create(form);
                var check = Logic.BusinessTypeService.CreateExists(model);
                if (check)
                {
                    return BadRequest("BusinessType already exists");
                }
                var dto = Logic.BusinessTypeService.Insert(model);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update BusinessType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("Update/{id}")]
        public IActionResult Update(int id, BusinessTypeForm form)
        {
            try
            {
                var model = Logic.BusinessTypeService.Create(form);
                if (id != model.Id)
                    return BadRequest("Route Parameter does mot match model ID");
                var found = Logic.BusinessTypeService.Get(id);
                if (found == null)
                    return NotFound();
                var check = Logic.BusinessTypeService.UpdateExists(model);
                if (Logic.BusinessTypeService.UpdateExists(model))
                    return BadRequest("BusinessType configuration already exists");
                var dto = Logic.BusinessTypeService.Update(found, model,
                    "Name,MinStakeHolder,MaxStakeHolder,MinCapital,Info,RecordStatus");
                return Ok(dto);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete BusinessType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var found = Logic.BusinessTypeService.Get(id);
                if (found == null)
                    return NotFound();
                Logic.BusinessTypeService.Delete(found);
                return NoContent();
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}