using Autofac;
using MicroService.Core;
using MicroService.Data;
using MicroService.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace MicroService.ServerHost.Org
{
   public class DefaultModuleRegister: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var baseType = typeof(IDependency);
            string path = Assembly.GetExecutingAssembly().Location;
            Assembly[] assemblies = AssemblyHelper.GetAssemblyList(path).ToArray();
            builder.RegisterAssemblyTypes(assemblies)
               .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
               .AsSelf()   //自身服务，用于没有接口的类
               .AsImplementedInterfaces()  //接口服务
               .PropertiesAutowired()  //属性注入
               .SingleInstance();    //保证生命周期基于请求
        }

       
    }
}
