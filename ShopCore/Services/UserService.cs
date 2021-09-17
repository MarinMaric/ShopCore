using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Exceptions;
using ShopCore.WebAPI.Requests;

namespace ShopCore.WebAPI.Services
{
    public class UserService : IUserService
    {
        public IList<User> Get()
        {
            var list = VirtualDB.Users;
            return list;
        }

        public User GetById(int id)
        {
            var user = VirtualDB.Users.Where(x => x.UserID == id).FirstOrDefault();

            return user;
        }

        public User Insert(UserInsertRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new UserException("Passwords don't match!");

            if (VirtualDB.Users.Count > 0)
                if (VirtualDB.Users.Where(x => x.Username == request.Username).FirstOrDefault() != null)
                    throw new UserException("Username taken!");

            var salt = GenerateSalt();

            User user = new User()
            {
                UserID = VirtualDB.Users.Count(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Image = "/Images/Basic/default-avatar.png",
                PasswordSalt = salt,
                PasswordHash = GenerateHash(salt, request.Password),
                Admin = false,
                Deleted = false
            };

            VirtualDB.Users.Add(user);
            return user;
        }

        public User Update(UserEditRequest request)
        {
            var checkTaken = VirtualDB.Users.Where(x => x.Username == request.Username).FirstOrDefault();

            if (checkTaken != null && checkTaken.Username!=request.OriginalUsername)
                throw new UserException("Username taken!");

            User user = VirtualDB.Users.Where(x => x.Username == request.OriginalUsername).FirstOrDefault();

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            user.Email = request.Email;
            var salt = GenerateSalt();
            user.PasswordSalt = salt;
            user.PasswordHash = GenerateHash(salt, request.Password);
            if(request.Image!=null)
                user.Image = request.Image;

            return user;
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
    }
}
