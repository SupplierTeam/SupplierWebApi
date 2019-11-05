using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.Models.Dto
{
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets 用户名.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets 密码.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets 系统编号.
        /// </summary>
        public string SystemId { get; set; }
    }
}
