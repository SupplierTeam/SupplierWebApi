using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.Framework
{
    public class ConfigurationHelper
    {
        public static IConfiguration config { get; set; }

        static ConfigurationHelper()
        {
            config =MyServiceProvider.ServiceProvider.GetRequiredService<IConfiguration>();
        }

        public static string GetConnectionString()
        {
            return JsonHelper.NewtonsoftDeserialize<ConfigMsg>(config.GetSection("AppSetting").Value).ConnectionStrings;
        }

        public static string GetRedisConnectionString()
        {
            return JsonHelper.NewtonsoftDeserialize<ConfigMsg>(config.GetSection("AppSetting").Value).RedisAddress;
        }

    }
}
