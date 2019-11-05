using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.Models.Dto
{
    public class LoginUserDto
    {
        public string UserName { get; set; }

        public string UserId { get; set; }

        public string SystemId { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string QQ { get; set; }

        public string Remark { get; set; }
    }
}
