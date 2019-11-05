using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace SupplierWebApi.Tests.DependencyInjection
{
    /// <summary>
    /// 依赖注入测试
    /// </summary>
    public class DI_Test
    {

        [Fact]
        public void DI_Connet_Test()
        {
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            //指定已扫描程序集中的类型注册为提供所有其实现的接口。
            var path = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;//获取项目路径
            var servicesDllFile = Path.Combine(path, "SupplierWebApi.Services.dll");//获取注入项目绝对路径
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();
            var assemblysRepository = Assembly.LoadFile(Path.Combine(path, "SupplierWebApi.Repositories.dll"));
           
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            Assert.True(ApplicationContainer.ComponentRegistry.Registrations.Count() > 1);
        }
    }
}
