using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Com.Ctrip.Framework.Apollo;
using SupplierWebApi.Framework;
using SupplierWebApi.Models;

namespace SupplierWebApi.Dapper.Connections
{
    public class ConfigurationHelper
    {
        public IConfiguration config { get; set; }

        public ConfigurationHelper()
        {
            config = MyServiceProvider.ServiceProvider.GetRequiredService<IConfiguration>();
        }

        public string GetConnectionString()
        {
            return JsonHelper.NewtonsoftDeserialize<ConfigMsga>(config.GetSection("AppSetting").Value).ConnectionStrings;
        }
    }
}
