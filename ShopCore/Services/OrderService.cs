using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Requests;

namespace ShopCore.WebAPI.Services
{
    public class OrderService : IOrderService
    {
        public List<Product> AddToCart(List<Product> cart, int pID)
        {
            var p = VirtualDB.Products.Where(x => x.ProductID == pID).FirstOrDefault();
            if(p!=null)
                cart.Add(p);
            return cart;
        }

        public List<Product> RemoveFromCart(List<Product> cart, int pID)
        {
            var p = cart.Where(x => x.ProductID == pID).FirstOrDefault();
            if(p!=null)
                cart.Remove(p);
            return cart;
        }

        public List<Product> CompleteOrder(List<Product> cart, int uID)
        {
            Order order = new Order()
            {
                OrderID = VirtualDB.Orders.Count,
                CustomerID = uID,
                OrderDate = DateTime.Now
            };

            VirtualDB.Orders.Add(order);

            foreach (Product p in cart)
            {
                var item = new OrderItem()
                {
                    OrderID = order.OrderID,
                    ProductID = p.ProductID
                };

                VirtualDB.OrderItems.Add(item);
            }

            return cart;
        }

        public List<Order> GetOrders(int uID)
        {
            return VirtualDB.Orders.Where(x => x.CustomerID == uID).ToList();
        }

        public List<Product> GetOrderByID(int id)
        {
            List<OrderItem> items = VirtualDB.OrderItems.Where(x => x.OrderID == id).ToList();
            List<Product> products = new List<Product>();

            foreach(var i in items)
            {
                products.Add(VirtualDB.Products.Where(x => x.ProductID == i.ProductID).FirstOrDefault());
            }

            return products;
        }
    }
}
