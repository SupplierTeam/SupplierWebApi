using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.Framework
{
   public static class EnvironmentExtention
    {
        public static string GetEnvironmentName(this IHostBuilder hostBuilder)
        {
            var env=string.Empty;
            hostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                // this works with Microsoft.Extensions.Hosting.Abstractions installed
                 env = context.HostingEnvironment.EnvironmentName;
            });

            return env;
        }
    }
}
