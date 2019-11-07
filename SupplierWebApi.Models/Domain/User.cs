using System;
using Dapper.Contrib.Extensions;


namespace SupplierWebApi.Models.Domain
{
    [Table("User")]
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int IsDelete { get; set; }

        public string Email { get; set; }

        public string TelPhone { get; set; }

        public string Remark { get; set; }

        public DateTime? AddTime { get; set; }
    }
}
