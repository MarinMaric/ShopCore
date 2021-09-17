using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Services;
using ShopCore.WebAPI.Requests;
using Newtonsoft.Json;
using ShopCore.WebAPI.Exceptions;

namespace ShopCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService = null;

        public LoginController(ILoginService service)
        {
            _loginService = service;

        }

        [HttpPost]
        public string Authenticate([FromBody] UserLoginRequest request)
        {
            User u = _loginService.Authenticate(request);
            if (u == null)
            {
                throw new UserException("Wrong username or password!");
            }

            return JsonConvert.SerializeObject(u);
        }

        [HttpPost]
        [Route("authorize")]
        public IActionResult Authorize([FromBody] int id)
        {
            if (_loginService.Authorize(id))
                return Ok();
            else  return NotFound();
        }
    }
}