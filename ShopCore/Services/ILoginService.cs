using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Requests;

namespace ShopCore.WebAPI.Services
{
    public interface ILoginService
    {
        User Authenticate(UserLoginRequest request);
        bool Authorize(int ? id);
    }
}
