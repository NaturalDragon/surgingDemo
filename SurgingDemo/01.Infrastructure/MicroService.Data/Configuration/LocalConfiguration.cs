using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Configuration
{
    public class LocalConfiguration
    {

        private static string ENVIRONMENT = "release";

        protected IConfigurationRoot ConfigRoot { set; get; }

        protected bool IsRemote { set; get; }


        public LocalConfiguration()
        {

#if DEBUG
            ENVIRONMENT = "debug";
#endif
            var fileName = string.Format("appsettings.{0}.json", ENVIRONMENT);

            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory))
                .AddJsonFile(fileName, optional: false, reloadOnChange: true);

            this.ConfigRoot = builder.Build();

            this.IsRemote = this.ConfigRoot.GetValue<bool>("RemoteConfig");

        }

        public virtual T GetEntity<T>(string key) where T : new()
        {
            T entity = new T();
            this.ConfigRoot.GetSection(string.IsNullOrWhiteSpace(key) ? "App" : key).Bind(entity);
            return entity;

        }

        public virtual T GetValue<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return default(T);

            return this.ConfigRoot.GetValue<T>(key);
        }

    }
}
