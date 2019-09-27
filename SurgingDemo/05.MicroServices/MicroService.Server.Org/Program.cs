using Autofac;
using Microsoft.Extensions.Logging;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.ProxyGenerator;
using Surging.Core.ServiceHosting.Internal.Implementation;
using System;
using System.Text;
using Surging.Core;
using Surging.Core.ServiceHosting;
using Surging.Core.Caching.Configurations;
using Surging.Core.CPlatform.Configurations;
using MicroService.ServerHost.Org.Filter;
using Surging.Core.System.Intercept;
using MicroService.IModules.Org;

namespace MicroService.ServerHost.Org
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var host = new ServiceHostBuilder()
                .RegisterServices(builder =>
                {
                    builder.AddMicroService(option =>
                    {

                        option.AddServiceRuntime()
                        .AddRelateService()

                        .AddConfigurationWatch()
                        .AddFilter(new ServiceExceptionFilter())
                        //option.UseZooKeeperManager(new ConfigInfo("127.0.0.1:2181")); 
                       // .AddClientIntercepted(typeof(IntercepteModule))
                        .AddServiceEngine(typeof(SurgingServiceEngine))
                        ;
                        builder.Register(p => new CPlatformContainer(ServiceLocator.Current));
                    });
                })
                .ConfigureLogging(logger =>
                {
                    logger.AddConfiguration(
                        Surging.Core.CPlatform.AppConfig.GetSection("Logging"));
                })
                .UseServer(options => { })
                .UseConsoleLifetime()
                .Configure(build =>
                build.AddCacheFile("${cachepath}|cacheSettings.json", basePath: AppContext.BaseDirectory, optional: false, reloadOnChange: true))
                  .Configure(build =>
                build.AddCPlatformFile("${surgingpath}|surgingSettings.json", optional: false, reloadOnChange: true))
                .UseStartup<Startup>()
                .Build();

            using (host.Run())
            {
                Console.WriteLine($"服务端启动成功，{DateTime.Now}。");
            }
        }
    }
}
