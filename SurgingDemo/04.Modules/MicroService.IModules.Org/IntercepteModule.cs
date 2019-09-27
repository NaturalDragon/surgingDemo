using Surging.Core.CPlatform.Module;
using Surging.Core.ProxyGenerator;
using Surging.Core.System.Intercept;

namespace MicroService.IModules.Org
{
    /// <summary>
    /// 
    /// </summary>
    public class IntercepteModule: SystemModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appModule"></param>
        public override void Initialize(AppModuleContext  appModule)
        {
            base.Initialize(appModule);
        }

        /// <summary>
        /// Inject dependent third-party components
        /// </summary>
        /// <param name="builder"></param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
           
            builder.AddClientIntercepted(typeof(CacheProviderInterceptor));
        }
    }
}