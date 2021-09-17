using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Exceptions;
using ShopCore.WebAPI.Requests;

namespace ShopCore.WebAPI.Services
{
    public class LoginService : ILoginService
    {
        public User Authenticate(UserLoginRequest request)
        {
            var user = VirtualDB.Users.Where(x => x.Username == request.Username).FirstOrDefault();

            if (user != null)
            {
                var hash = UserService.GenerateHash(user.PasswordSalt, request.Password);
                if (user.PasswordHash == hash)
                    return user;
            }
            return null;
        }

        public bool Authorize(int ? id)
        {
            if (id != null)
            {
                var u = VirtualDB.Users.Where(x => x.UserID == id).FirstOrDefault();
                return u.Admin;
            }
            else return false;
        }
    }
}
