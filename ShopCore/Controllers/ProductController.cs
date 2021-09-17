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
using System.Net.Http;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ShopCore.WebAPI.Exceptions;

namespace ShopCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private  IHostingEnvironment _hostingService;
        private ILoginService _loginService;

        public ProductController(IProductService productService, IHostingEnvironment hostingService, ILoginService loginService)
        {
            _productService = productService;
            _hostingService = hostingService;
            _loginService = loginService;
        }

        [HttpGet]
        public  ActionResult<string> Get()
        {
            return Ok(JsonConvert.SerializeObject(_productService.Get()));   
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            var item = _productService.GetById(id);

            if(item!=null)
                return JsonConvert.SerializeObject(item);
            else
                return NotFound(item);
        }

        [HttpGet]
        [Route("name")]
        public ActionResult<string> GetByName([FromHeader]string name)
        {
            if (name == null)
                return JsonConvert.SerializeObject(_productService.Get());
            List<Product> all = _productService.Get();

            return JsonConvert.SerializeObject(all.Where(x => x.Name.Contains(name.ToUpper())));
        }

        [HttpPost]
        public IActionResult Insert([FromForm] int uid, [FromForm] string name, [FromForm] string developer, [FromForm] string publisher, [FromForm] decimal price, [FromForm] string description, [FromForm] DateTime releaseDate, List<IFormFile> Image)
        {
            if (!_loginService.Authorize(uid))
                return Redirect("https://localhost:44344/api" + "/profile.html");

            ProductInsertRequest p = new ProductInsertRequest()
            {
                Name = name.ToUpper(),
                Developer = developer,
                Publisher = publisher,
                Price = price,
                Description = description,
                ReleaseDate = releaseDate
            };

            var uploads = Path.Combine(_hostingService.WebRootPath, "Images");
            foreach (var img in Image)
            {
                if (img.Length > 0)
                {
                    var imgPath = Path.Combine(uploads, img.FileName);
                    using (var fileStream = new FileStream(imgPath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }

                    p.Image = "/Images/" + img.FileName;
                }
            }

            try {
                _productService.Insert(p);
            }
            catch (UserException err)
            {
                Response.WriteAsync("<script language='javascript'>alert('" + err.Message + "');window.location.replace('https://localhost:44318/insertProduct.html')</script>");
            }
            
            return Redirect("https://localhost:44318/products.html");
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromForm] int uid, [FromForm] int pid, [FromForm] string name, [FromForm] string developer, [FromForm] string publisher, [FromForm] decimal price, [FromForm] string description, [FromForm] DateTime ? releaseDate, List<IFormFile>image)
        {
            if (!_loginService.Authorize(uid))
                return Redirect("https://localhost:44318/login.html");

            ProductInsertRequest request = new ProductInsertRequest()
            {
                Name = name.ToUpper(),
                Developer = developer,
                Publisher = publisher,
                Price = price,
                Description = description,
            };

            if (releaseDate != null)
                request.ReleaseDate = (DateTime)releaseDate;

            if (image.Count == 0)
                request.Image = null;
            else
            {
                var uploads = Path.Combine(_hostingService.WebRootPath, "Images/");
                foreach (var img in image)
                {
                    if (img.Length > 0)
                    {
                        var imgPath = Path.Combine(uploads, img.FileName);
                        using (var fileStream = new FileStream(imgPath, FileMode.Create))
                        {
                            img.CopyTo(fileStream);
                        }
                        request.Image = "/Images/" + img.FileName;
                    }
                }
            }

            try
            {
                _productService.Update(pid, request);
            }
            catch(UserException err)
            {
                Response.WriteAsync("<script language='javascript'>alert('" + err.Message + "');window.location.replace('https://localhost:44318/editProduct.html')</script>");
            }
            return Redirect("https://localhost:44318/products.html");
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]int pid)
        {
            _productService.Delete(pid);
            return Ok();
        }

    }
}