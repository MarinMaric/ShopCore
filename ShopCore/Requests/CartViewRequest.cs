using ShopCore.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCore.WebAPI.Requests
{
    public class CartViewRequest
    {
        public List<Product> CartItems { get; set; }
        public List<int> Quantities { get; set; }

        public CartViewRequest()
        {
            CartItems = new List<Product>();
            Quantities = new List<int>();
        }

        public CartViewRequest(List<Product> CartItems, List<int>Quantities)
        {
            this.CartItems = CartItems;
            this.Quantities = Quantities;
        }

        public CartViewRequest(CartViewRequest cart)
        {
            this.CartItems = cart.CartItems;
            this.Quantities = cart.Quantities;
        }
    }
}
