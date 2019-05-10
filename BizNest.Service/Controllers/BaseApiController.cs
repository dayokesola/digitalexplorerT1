using BizNest.Core.Logic.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BizNest.Service.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private LogicModule _module;

        /// <summary>
        /// 
        /// </summary>
        public LogicModule Logic
        {
            get
            {
                if (_module == null)
                {
                    _module = new LogicModule();
                }
                return _module;
            }
        }

        /// <summary>
        /// Adds the header.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected void AddHeader(string key, object data)
        {

            //HttpContext.Current.Response.Headers.Add(key, Util.SerializeJSON(data));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public string ModelError(ModelStateDictionary modelState)
        {
            string error = "";
            foreach (var state in modelState.Values)
            {
                foreach (var msg in state.Errors)
                {
                    error += msg.ErrorMessage + "<br />";
                }
            }
            return error;
        }

        //private LoggedInUserModel _loggedInUser;

        ///// <summary>
        ///// 
        ///// </summary>
        //public LoggedInUserModel LoggedInUser
        //{
        //    get
        //    {
        //        if (_loggedInUser == null)
        //        {
        //            _loggedInUser = Logic.FactoryModule.SiteUsers.Profile();
        //        }
        //        return _loggedInUser;
        //    }
        //}
    }
}