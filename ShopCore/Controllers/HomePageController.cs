using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopCore.WebAPI.Database;

namespace ShopCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private IHostingEnvironment _hostingService;

        public HomePageController(IHostingEnvironment hostingService)
        {
            _hostingService = hostingService;
        }

        [HttpGet]
        public string GetHots()
        {
            return nones();
        }

        string getOg()
        {
            var imagesAll = Directory.GetFiles(_hostingService.WebRootPath + "/Images").ToList();
            int size = imagesAll.Count;
            Random random = new Random();

            var returnList = new List<string>();
            var returnedIndexes = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                bool duplicate = true;
                int rand = 0;

                while (duplicate)
                {
                    bool found = false;
                    rand = random.Next(0, size);
                    foreach (int n in returnedIndexes)
                    {
                        if (n == rand)
                            found = true;
                    }
                    if (!found)
                    {
                        duplicate = false;
                        returnedIndexes.Add(rand);
                    }
                }

                var relPath = "/Images/" + Path.GetFileName(imagesAll[rand]);

                foreach (string item in returnList)
                {
                    if (item == imagesAll[rand])
                        duplicate = true;
                }

                if (!duplicate)
                    returnList.Add(relPath);
            }

            var returnIds = new List<int>();

            foreach (var img in returnList)
            {
                returnIds.Add(VirtualDB.Products.Where(x => x.Image == img).First().ProductID);
            }

            var imageLinkCombo = new
            {
                images = returnList.ToArray(),
                ids = returnIds.ToArray()
            };

            return JsonConvert.SerializeObject(imageLinkCombo);
        }

        string nones()
        {
            Random random = new Random();
            var returnList = new List<string>();
            var returnedIndexes = new List<int>();

            var selectedProducts = new List<Product>();
            var selectedProductIndexes = new List<int>();

            int  numberOfIterations = 3;
            if (VirtualDB.Products.Count < 3)
                numberOfIterations = VirtualDB.Products.Count;

            for (int i = 0; i<numberOfIterations; i++)
            {
                bool duplicate = true;
                int rand = 0;

                while (duplicate)
                {
                    bool found = false;
                    rand = random.Next(0, VirtualDB.Products.Count);
                    foreach (int n in selectedProductIndexes)
                    {
                        if (n == rand)
                            found = true;
                    }
                    if (!found)
                    {
                        duplicate = false;
                        selectedProductIndexes.Add(rand);
                        selectedProducts.Add(VirtualDB.Products[rand]);
                    }
                }
            }

            var returnIds = new List<int>();

            foreach(Product p in selectedProducts)
            {
                returnList.Add(p.Image);
                returnIds.Add(p.ProductID);
            }

            var imageLinkCombo = new
            {
                images = returnList.ToArray(),
                ids = returnIds.ToArray()
            };

            return JsonConvert.SerializeObject(imageLinkCombo);
        }

        [HttpPost]
        public string SetLookup([FromBody]int id)
        {
            HttpContext.Session.SetInt32("productLookup", id);
            return JsonConvert.SerializeObject(id);
        }
    }
}