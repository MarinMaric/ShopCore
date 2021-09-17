using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Exceptions;
using ShopCore.WebAPI.Requests;
using ShopCore.WebAPI.Services;

namespace ShopCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public class RequestCart
        {
            public List<Product> cart { get; set; }
            public int pID { get; set; }
        }
        public class RequestOrder
        {
            public List<Product> cart { get; set; }
            public int uID { get; set; }
        }

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            //CartViewRequest cart = new CartViewRequest();
            return Ok(JsonConvert.SerializeObject(new List<Product>()));
        }

        [HttpPost]
        [Route("view")]
        public IActionResult GetOrders([FromBody]int uID)
        {
            return Ok(JsonConvert.SerializeObject(_orderService.GetOrders(uID)));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            return Ok(JsonConvert.SerializeObject(_orderService.GetOrderByID(id)));
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody]RequestCart request)
        {
            if (request.cart == null)
                request.cart = new List<Product>();
            return Ok(JsonConvert.SerializeObject(_orderService.AddToCart(request.cart, request.pID)));
        }

        [HttpDelete]
        public IActionResult RemoveFromCart([FromBody]RequestCart request)
        {
            return Ok(JsonConvert.SerializeObject(_orderService.RemoveFromCart(request.cart, request.pID)));
        }

        [HttpPost]
        [Route("complete")]
        public IActionResult CompleteOrder([FromBody]RequestOrder order)
        {
            if(order.cart==null || order.cart.Count==0)
                throw new UserException("Cart can't be empty!");

            return Ok(_orderService.CompleteOrder(order.cart, order.uID));
        }
    }
}