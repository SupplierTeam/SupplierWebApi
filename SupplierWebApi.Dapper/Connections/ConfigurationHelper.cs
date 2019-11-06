using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace SupplierWebApi.Dapper.Connections
{
    public class ConfigurationHelper
    {
        public IConfiguration config { get; set; }

        public ConfigurationHelper()
        {
            IHostEnvironment env = MyServiceProvider.ServiceProvider.GetRequiredService<IHostEnvironment>();
            config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public string GetConnectionString()
        {
            return config.GetSection("ConnectionStrings")["SupBack"];
        }
    }
}
