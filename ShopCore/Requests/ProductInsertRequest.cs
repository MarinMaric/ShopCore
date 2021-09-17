using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShopCore.WebAPI.Requests
{
    public class ProductInsertRequest
    {
        //[Required(AllowEmptyStrings =false)]
        public string Name { get; set; }
        //[Required(AllowEmptyStrings =false)]
        public string Developer { get; set; }
        //[Required(AllowEmptyStrings =false)]
        public string Publisher { get; set; }

        //[Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
    }
}
