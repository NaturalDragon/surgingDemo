using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Configuration
{
    public class ConfigManager//<T> where T : new()
    {
        static AppConfiguration AppConfig { set; get; }
        static readonly Object lockObj = new Object();

        /// <summary>
        /// 当前微服务名称
        /// </summary>
        public static string ServiceName { set; get; }

        static ConfigManager()
        {
            if (AppConfig == null)
            {
                lock (lockObj)
                {
                    if (AppConfig == null)
                    {
                        AppConfig = new AppConfiguration();
                    }
                }
            }

            ServiceName = AppConfig.GetValue<string>("spring:application:name");

        }

        /// <summary>
        /// 获取配置并转换为对应的实体对象
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static T GetEntity<T>(string key = null) where T : new()
        {
            return AppConfig.GetEntity<T>(key);
        }

        /// <summary>
        /// 获取配置并转换为指定类型的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            return AppConfig.GetValue<T>(key);
        }

    }
}
