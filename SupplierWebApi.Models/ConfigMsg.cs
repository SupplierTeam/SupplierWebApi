using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.Models
{
    public class ConfigMsg
    {
        public string ConnectionStrings { get; set; }
        public string AuthorithApiAddress { get; set; }
        
        public string AuthorizationCenterAddress { get; set; }
        public string RedisAddress { get; set; }

        public string RedirectLogin { get; set; }
        public string RedirectError { get; set; }
        public string WelfareAddress { get; set; }
        public string JobAddress { get; set; }


        public string BuyCardEmail { get; set; }
        public string FtpAddress { get; set; }
        public string FtpUser { get; set; }
        public string FtpPwd { get; set; }

        public int FtpPort { get; set; }
        public string LogsAppkey { get; set; }
        public string LogsAppServer { get; set; }

        public string SystemId { get; set; }

    }
}
