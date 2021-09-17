using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCore.WebAPI.Database
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string Image { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
