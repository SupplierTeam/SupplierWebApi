using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Fairhr.ApiGateWay;
using Fairhr.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SupplierWebApi.Framework;
using SupplierWebApi.Models;
using SupplierWebApi.Models.DataContext;

namespace SupplierWebApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }

      

        /// <summary>
        /// Autofac����ע��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {


            #region Autofac
            var path = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;//��ȡ��Ŀ·��
            var servicesDllFile = Path.Combine(path, "SupplierWebApi.Services.dll");//��ȡע����Ŀ����·��
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//ֱ�Ӳ��ü����ļ��ķ���

            var assemblysRepository = Assembly.LoadFile(Path.Combine(path, "SupplierWebApi.Repositories.dll"));//ģʽ�� Load(���������)
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces().PropertiesAutowired()
                      //.InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors();//����Autofac.Extras.DynamicProxy;
                                                     //.InterceptedBy(typeof(UserLogAop));//����ֱ���滻������

            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces().PropertiesAutowired();
            #endregion

        }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //����ע��õ�Apollo������ʵ��������key��AppSetting����ȡ��Ӧ�������ļ���value������json���л�Ϊ����
            var config = JsonHelper.NewtonsoftDeserialize<ConfigMsg>(Configuration.GetSection("AppSetting").Value);

            //��apollo�������ļ���Json��ʽ����Ŀ�н������ڵ�ʵ��
            services.ConfigureJsonValue<ConfigMsg>(Configuration.GetSection("AppSetting"));

            #region Mysql
            services.AddDbContextPool<SupplierdbContext>(options =>
            {

                options.UseMySql(config.ConnectionStrings);
                options.EnableSensitiveDataLogging(true);
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = "SupplierWebApi API",
                    Description = "���˵���ĵ�",   

                });
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "SupplierWebApi.xml");//������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�
                var xmlModelPath = Path.Combine(basePath, "SupplierWebApi.Models.xml");//�������Model���xml�ļ���

                c.IncludeXmlComments(xmlModelPath);

             

               
                //���header��֤��Ϣ
                var security = new OpenApiSecurityRequirement{ 
                    
                };
                c.AddSecurityRequirement(security);//���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������Ҫһ�£�������Bearer��
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) �����ṹ: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                }); ;

            });

            #endregion

            #region FairLogs
            services.AddFairhrLogs(options =>
            {
                options.Key = config.LogsAppkey;
                options.ServerUrl = config.LogsAppServer;
            });
            #endregion

            #region FarihrGetWay
            services.AddFarihrGetWay(options =>
            {
                options.AppKey = "203749203";
                options.AppSecret = "u7a2xn3g8bt7saylu25vyp561fvpnwt9";
                // api���ص�ַ
                options.Host = "http://api.fanyouvip.com";
                // ���û�������
                options.Environment = "TEST";

            });
            #endregion

            services.AddControllers();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                c.RoutePrefix = "";

            });
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            #region FairLogs
            app.UseFairhrLogs();
            //ȫ���쳣��
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    FairhrLogs.Error("�쳣��" + context.Features.Get<IExceptionHandlerFeature>().Error.Message);
                    ResultData result = new ResultData()
                    {

                        Code = 500,
                        Msg = context.Features.Get<IExceptionHandlerFeature>().Error.Message,
                        Count = 0,
                        Data = ""
                    };
                    await context.Response.WriteAsync(JsonHelper.NewtonsoftSerialiize(result));
                });
            });
            #endregion


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SupplierdbContext>
    {

        public SupplierdbContext CreateDbContext(string[] args)
        {
            var environment=Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT ");
         
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile($"appsettings.Development.json")//Ӧ���޸�Ϊ����������appsettings.json
           .Build();
            var builder = new DbContextOptionsBuilder<SupplierdbContext>();
            builder.UseMySql(configuration.GetConnectionString("SupBack"));
            return new SupplierdbContext(builder.Options);
        }
    }

}
