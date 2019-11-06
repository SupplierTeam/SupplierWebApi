using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Com.Ctrip.Framework.Apollo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SupplierWebApi.Framework;

namespace SupplierWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())//Autofac����ע��
        .ConfigureWebHostDefaults(webHostBuilder =>
        {
            webHostBuilder
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseIISIntegration()
              .ConfigureAppConfiguration(builder => builder
              .AddApollo(builder.Build().GetSection("apollo"))//����apollo������Com.Ctrip.Framework.Apollo
              //.AddNamespace("fanyou") // �����ռ�
              .AddDefault())
              .UseStartup<Startup>();
        })
        
        .Build();

            host.Run();
        }
    }

       
}
