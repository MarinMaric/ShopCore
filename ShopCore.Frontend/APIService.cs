using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;

namespace ShopCore.Frontend
{
    public class APIService
    {
        private string _route = null;

        public APIService(string route)
        {
            _route = route;
        }

    }
}
