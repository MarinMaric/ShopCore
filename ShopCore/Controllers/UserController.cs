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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ShopCore.WebAPI.Exceptions;

namespace ShopCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IHostingEnvironment _hostingService;

        public UserController(IUserService userService, IHostingEnvironment hostingService)
        {
            _userService = userService;
            _hostingService = hostingService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(JsonConvert.SerializeObject(_userService.Get()));
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            //if (id == -1)
            //{
            //    var temp = HttpContext.Session.GetInt32("ActiveUser");
            //    if (temp == null)
            //    {
            //        return JsonConvert.SerializeObject(null);
            //    }
            //    id = (int)temp;
            //}

            var item = _userService.GetById(id);

            if (item != null)
                return Ok(JsonConvert.SerializeObject(item));
            else
                throw new UserException("cookie ne valja");
        }        

        [HttpPost]
        public string Insert([FromBody] UserInsertRequest request)
        {
            return JsonConvert.SerializeObject(_userService.Insert(request));
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromForm] string ogUsername, [FromForm] string firstName, [FromForm] string lastName, [FromForm] string username, [FromForm] string email, [FromForm] string password, [FromForm] string confirmPassword, List<IFormFile> Image)
        {
            try
            {
                if (password == null)
                    throw new UserException("Enter password!");
                if (password != confirmPassword)
                    throw new UserException("Passwords not matching!");
            }
            catch(UserException err)
            {
                var response = Response.WriteAsync("<script language='javascript'>alert('" + err.Message + "');window.location.replace('https://localhost:44318/profile.html');</script>");
                return Redirect("https://localhost:44318/profile.html");
            }

            UserEditRequest request = new UserEditRequest()
            {
                OriginalUsername = ogUsername,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            if (Image.Count == 0)
                request.Image = null;
            else {
                var uploads = Path.Combine(_hostingService.WebRootPath, "Images/Avatars");
                foreach (var img in Image)
                {
                    if (img.Length > 0)
                    {
                        var imgPath = Path.Combine(uploads, img.FileName);
                        using (var fileStream = new FileStream(imgPath, FileMode.Create))
                        {
                            img.CopyTo(fileStream);
                        }
                        request.Image = "/Images/Avatars/" + img.FileName;
                    }
                }
            }
            try
            {
                _userService.Update(request);
            }
            catch(UserException err)
            {
                Response.WriteAsync("<script language='javascript'>window.location.replace('https://localhost:44318/profile.html');alert('" + err.Message + "');window.location.replace('https://localhost:44318/profile.html');</script>");
            }
            return Redirect("https://localhost:44318/profile.html");
        }
    }
}