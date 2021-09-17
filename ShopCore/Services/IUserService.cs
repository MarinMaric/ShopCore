using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Requests;

namespace ShopCore.WebAPI.Services
{
    public interface IUserService
    {
        IList<User> Get();
        User GetById(int id);
        User Insert(UserInsertRequest request);
        User Update(UserEditRequest request);
    }
}
