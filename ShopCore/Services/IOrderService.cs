using ShopCore.WebAPI.Requests;
using ShopCore.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCore.WebAPI.Services
{
    public interface IOrderService
    {
        List<Product> AddToCart(List<Product> cart, int pID);
        List<Product> RemoveFromCart(List<Product> cart, int pID);
        List<Product> CompleteOrder(List<Product> cart, int uID);
        List<Order> GetOrders(int uID);
        List<Product> GetOrderByID(int id);
    }
}
