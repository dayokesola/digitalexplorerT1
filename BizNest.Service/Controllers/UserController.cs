using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BizNest.Core.Domain.Entity.App;
using BizNest.Service.Helpers;
using BizNest.Service.Interfaces;
using BizNest.Service.Models.AuthModels;
using BizNest.Service.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BizNest.Service.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IConfiguration _configuration;
       

        public UserController(
              UserManager<ApplicationUser> userManager,
              IOptions<JwtIssuerOptions> jwtOptions,
              IJwtFactory jwtFactory,
              IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = "Your Email or Password is Incorrect"
                });
            }
            return Ok(new RegisterResponse
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,

            });
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var identity = await GetClaimsIdentity(model.Email, model.Password);

                if (identity == null)
                {
                    return BadRequest(new ErrorResponse
                    {
                        ErrorDescription = "Your Email or Password is Incorrect"
                    });
                }
                var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return Ok(new LoginResponse
                {
                    Token = jwt
                });
            }

            return BadRequest(new ErrorResponse
            {
                ErrorDescription = "Your Email or Password is Incorrect"
            });

        }

        [AllowAnonymous]
        [HttpPost("system")]
        public async Task<IActionResult> System(SysLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var identity = await GetSysClaimsIdentity(model.AppName, model.AppKey);

                if (identity == null)
                {
                    return BadRequest(new ErrorResponse
                    {
                        ErrorDescription = "Unauthorized!"
                    });
                }
                var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.AppName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return Ok(new LoginResponse
                {
                    Token = jwt
                });
            }

            return BadRequest(new ErrorResponse
            {
                ErrorDescription = "Your Email or Password is Incorrect"
            });

        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByEmailAsync(email);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(email, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private async Task<ClaimsIdentity> GetSysClaimsIdentity(string appname, string appkey)
        {
            if (string.IsNullOrEmpty(appname) || string.IsNullOrEmpty(appkey))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var key = _configuration.GetSection("Applications")?.GetValue<string>(appname.ToLower());

            if (appkey != key || appkey == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            else
            {
                return await Task.FromResult(_jwtFactory.GenerateSysClaimsIdentity(appname, appkey));
            }

        }


    }
}