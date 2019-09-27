using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MicroService.EntityFramwork;
using MicroService.EntityFramwork.Mysql;
using MicroService.Application.Org;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Surging.Core.Caching.Configurations;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.EventBusKafka.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.ServerHost.Org
{
    public class Startup
    {
        public Startup(IConfigurationBuilder config)
        {
            ConfigureEventBus(config);
            //  ConfigureCache(config);
        }

        public IContainer ConfigureServices(ContainerBuilder builder)
        {
            var services = new ServiceCollection();
            var factoryContext = new FactoryUnitOfWorkDbContext();
            factoryContext.AddDbContext(services);
            // services.AddDbContext<IUnitOfWorkDbContext, MySqlDbContext>();
            // services.AddDbContext<MySqlDbContext>();
            services.AddAutoMapper();
            //  services.AddScoped<IUnitOfWorkDbContext, MySqlDbContext>();
           // services.AddScoped<TestDbContext>();
            ConfigureLogging(services);
            builder.Populate(services);
            //依赖注入
            builder.RegisterModule<DefaultModuleRegister>();

            ServiceLocator.Current = builder.Build();
            return ServiceLocator.Current;
        }

        public void Configure(IContainer app)
        {

        }

        #region 私有方法
        /// <summary>
        /// 配置日志服务
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureLogging(IServiceCollection services)
        {
            // services.AddLogging();
        }

        private static void ConfigureEventBus(IConfigurationBuilder build)
        {
            build
            .AddEventBusFile("eventBusSettings.json", optional: false);
        }

        /// <summary>
        /// 配置缓存服务
        /// </summary>
        private void ConfigureCache(IConfigurationBuilder build)
        {
            build
              .AddCacheFile("cacheSettings.json", optional: false);
        }
        #endregion

    }
}
