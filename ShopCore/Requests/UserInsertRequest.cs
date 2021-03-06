using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCore.WebAPI.Requests
{
    public class UserInsertRequest
    {
        [Required(AllowEmptyStrings =false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string ConfirmPassword { get; set; }
    }
}
